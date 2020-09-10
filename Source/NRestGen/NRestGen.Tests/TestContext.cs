using System;
using System.IO;
using System.Reflection;

namespace NRestGen.Tests
{
    internal static class TestContext
    {
        public static string BinDirectory
        {
            get
            {
                var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().CodeBase);
                var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
                var dirPath = Path.GetDirectoryName(codeBasePath);
                return dirPath;
            }
        }

        public static string GetDirectory(string relFolderName)
        {
            var dir = Path.Combine(BinDirectory, relFolderName);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            return dir;
        }

        public static string SolutionFolder => new Uri(Path.Combine(BinDirectory, @"..\..\..\..\")).AbsolutePath;
    }
}
