using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;

namespace NRestGen.TextTemplate
{
    // https://raw.github.com/damieng/DamienGKit
    // http://damieng.com/blog/2009/11/06/multiple-outputs-from-t4-made-easy-revisited

    public enum FileCreationMode
    {
        Diff,
        NotExists,
    }

    // MultiFileManager class records the various blocks so it can split them up
    public class MultiFileManager
    {
        private class Block
        {
            public String Name;
            public FileCreationMode FileMode;
            public bool InlcudeHeader;
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

        public void StartNewFile(String name, FileCreationMode fileMode = FileCreationMode.Diff)
        {
            if (name == null) { throw new ArgumentNullException(nameof(name)); }
            CurrentBlock = new Block { Name = name, FileMode = fileMode, InlcudeHeader = fileMode == FileCreationMode.Diff };
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
                    String content = String.Empty;
                    if (block.InlcudeHeader) content = Header;
                    content += template.ToString(block.Start, block.Length);

                    if (CreateFile(block.FileMode, fileName, content))
                        generatedFileNames.Add(fileName);
                    template.Remove(block.Start, block.Length);
                }
            }
        }

        private bool CreateFile(FileCreationMode fileMode, string fileName, string content)
        {
            switch (fileMode)
            {
                case FileCreationMode.NotExists:
                    return CreateFileIfNotExists(fileName, content);
                //case FileMode.Diff:
                default:
                    return CreateFileIfDiff(fileName, content);
            }
        }

        protected virtual bool CreateFileIfNotExists(String fileName, String content)
        {
            if (!File.Exists(fileName))
            {
                File.WriteAllText(fileName, content);
                return true;
            }
            return false;
        }

        protected virtual bool CreateFileIfDiff(String fileName, String content)
        {
            if (IsFileContentDifferent(fileName, content))
            {
                File.WriteAllText(fileName, content);
                return true;
            }
            return false;
        }

        public virtual String GetCustomToolNamespace(String fileName)
        {
            return String.Empty;
        }

        public virtual String DefaultProjectNamespace
        {
            get { return String.Empty; }
        }

        public virtual String ProjectDirectory
        {
            get { return Path.GetDirectoryName(this.host.TemplateFile); }
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

            public override String ProjectDirectory
            {
                get
                {
                    if (templateProjectItem == null) return base.ProjectDirectory;
                    return Path.GetDirectoryName(templateProjectItem.ContainingProject.FileName);
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

            protected override bool CreateFileIfNotExists(String fileName, String content)
            {
                if (!File.Exists(fileName))
                {
                    CheckoutFileIfRequired(fileName);
                    File.WriteAllText(fileName, content);
                    return true;
                }
                return false;
            }

            protected override bool CreateFileIfDiff(String fileName, String content)
            {
                if (IsFileContentDifferent(fileName, content))
                {
                    CheckoutFileIfRequired(fileName);
                    File.WriteAllText(fileName, content);
                    return true;
                }
                return false;
            }

            internal VSManager(EnvDTE.DTE dte, ITextTemplatingEngineHost host, StringBuilder template)
                : base(host, template)
            {
                this.dte = dte ?? throw new ArgumentNullException(nameof(dte), "EnvDTE.DTE must be specified.");

                templateProjectItem = dte.Solution.FindProjectItem(host.TemplateFile);
                checkOutAction = fileName => dte.SourceControl.CheckOutItem(fileName);
                projectSyncAction = keepFileNames => ProjectSync(templateProjectItem, keepFileNames);
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
