using Microsoft.VisualStudio.TextTemplating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NRestGen.TextTemplate
{
    // https://raw.github.com/damieng/DamienGKit
    // http://damieng.com/blog/2009/11/06/multiple-outputs-from-t4-made-easy-revisited

    // MultiFileManager class records the various blocks so it can split them up
    public class MultiFileManager
    {
        private class Block
        {
            public String Name;
            public int Start;
            public int Length;
        }

        private Block currentBlock;
        private readonly List<Block> files = new List<Block>();
        private readonly ITextTemplatingEngineHost host;
        private readonly StringBuilder template;
        protected readonly List<String> generatedFileNames = new List<String>();

        public static MultiFileManager Create(EnvDTE.DTE dte, ITextTemplatingEngineHost host, StringBuilder template)
        {
            return (host is IServiceProvider) ? new VSManager(dte, host, template) : new MultiFileManager(host, template);
        }

        public string Error { get; set; }

        public string Header { get; set; }

        public void StartNewFile(String name)
        {
            if (name == null) { throw new ArgumentNullException(nameof(name)); }
            CurrentBlock = new Block { Name = name };
        }

        public void EndBlock()
        {
            if (CurrentBlock == null)
                return;
            CurrentBlock.Length = template.Length - CurrentBlock.Start;
            files.Add(CurrentBlock);
            currentBlock = null;
        }

        public virtual void Process(bool split, bool sync = true)
        {
            if (split)
            {
                EndBlock();
                String outputPath = Path.GetDirectoryName(host.TemplateFile);
                files.Reverse();
                foreach (Block block in files)
                {
                    String fileName = Path.Combine(outputPath, block.Name);
                    String content = Header + template.ToString(block.Start, block.Length);
                    generatedFileNames.Add(fileName);
                    CreateFile(fileName, content);
                    template.Remove(block.Start, block.Length);
                }
            }
        }

        protected virtual void CreateFile(String fileName, String content)
        {
            if (IsFileContentDifferent(fileName, content))
                File.WriteAllText(fileName, content);
        }

        public virtual String GetCustomToolNamespace(String fileName)
        {
            return null;
        }

        public virtual String DefaultProjectNamespace
        {
            get { return null; }
        }

        protected static bool IsFileContentDifferent(String fileName, String newContent)
        {
            return !(File.Exists(fileName) && File.ReadAllText(fileName) == newContent);
        }

        private MultiFileManager(ITextTemplatingEngineHost host, StringBuilder template)
        {
            this.host = host;
            this.template = template;
            Header = String.Empty;
            Error = String.Empty;
        }

        private Block CurrentBlock
        {
            get { return currentBlock; }
            set
            {
                if (CurrentBlock != null)
                    EndBlock();
                if (value != null)
                    value.Start = template.Length;
                currentBlock = value;
            }
        }

        private class VSManager : MultiFileManager
        {
            private readonly EnvDTE.ProjectItem templateProjectItem;
            private readonly EnvDTE.DTE dte;
            private readonly Action<String> checkOutAction;
            private readonly Action<List<String>> projectSyncAction;

            public override String DefaultProjectNamespace
            {
                get
                {
                    if (templateProjectItem == null) return String.Empty;
                    return templateProjectItem.ContainingProject.Properties.Item("DefaultNamespace").Value.ToString();
                }
            }

            public override String GetCustomToolNamespace(string fileName)
            {
                if (dte == null) return String.Empty;
                return dte.Solution.FindProjectItem(fileName).Properties.Item("CustomToolNamespace").Value.ToString();
            }

            public override void Process(bool split, bool sync = true)
            {
                //if (templateProjectItem.ProjectItems == null)
                //    return;

                base.Process(split, sync);

                if (sync && projectSyncAction != null)
                    projectSyncAction(generatedFileNames);
            }

            protected override void CreateFile(String fileName, String content)
            {
                if (IsFileContentDifferent(fileName, content))
                {
                    CheckoutFileIfRequired(fileName);
                    File.WriteAllText(fileName, content);
                }
            }

            internal VSManager(EnvDTE.DTE dte, ITextTemplatingEngineHost host, StringBuilder template)
                : base(host, template)
            {
                //var hostServiceProvider = (IServiceProvider)host;
                //if (hostServiceProvider == null)
                //    throw new ArgumentException("Could not obtain IServiceProvider");

                // TODO: This throws an exception when running in VS!?
                // But not when debugguing...
                // dte = (EnvDTE.DTE)hostServiceProvider.GetService(typeof(EnvDTE.DTE));

                this.dte = dte;
                //if (this.dte == null)
                //    throw new ArgumentException("Could not obtain DTE from host");

                if (this.dte != null)
                {
                    templateProjectItem = dte.Solution.FindProjectItem(host.TemplateFile);
                    checkOutAction = fileName => dte.SourceControl.CheckOutItem(fileName);
                    projectSyncAction = keepFileNames => ProjectSync(templateProjectItem, keepFileNames);
                }
            }

            private static void ProjectSync(EnvDTE.ProjectItem templateProjectItem, List<String> keepFileNames)
            {
                if (templateProjectItem == null) return;

                var keepFileNameSet = new HashSet<String>(keepFileNames);
                var projectFiles = new Dictionary<String, EnvDTE.ProjectItem>();
                var originalFilePrefix = Path.GetFileNameWithoutExtension(templateProjectItem.FileNames[0]) + ".";
                foreach (EnvDTE.ProjectItem projectItem in templateProjectItem.ProjectItems)
                    projectFiles.Add(projectItem.FileNames[0], projectItem);

                // Remove unused items from the project
                foreach (var pair in projectFiles)
                    if (!keepFileNames.Contains(pair.Key) && !(Path.GetFileNameWithoutExtension(pair.Key) + ".").StartsWith(originalFilePrefix))
                        pair.Value.Delete();

                // Add missing files to the project
                foreach (String fileName in keepFileNameSet)
                    if (!projectFiles.ContainsKey(fileName))
                        templateProjectItem.ProjectItems.AddFromFile(fileName);
            }

            private void CheckoutFileIfRequired(String fileName)
            {
                if (dte != null)
                {
                    var sc = dte.SourceControl;
                    if (sc != null && sc.IsItemUnderSCC(fileName) && !sc.IsItemCheckedOut(fileName))
                        checkOutAction.EndInvoke(checkOutAction.BeginInvoke(fileName, null, null));
                }
            }
        }
    }
}
