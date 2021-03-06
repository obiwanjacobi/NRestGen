﻿namespace NRestGen.TextTemplate
{
    public sealed class GenSettings
    {
        public ProjectSettings Project { get; set; }
        public ApiSettings Api { get; set; }

        public ODataSettings Odata { get; set; }
        public MediatrSettings Mediatr { get; set; }

        public class ODataSettings
        {
            public bool Queryable { get; set; }
            public bool Select { get; set; }
            public bool Count { get; set; }
            public int Expand { get; set; }
            public int Max { get; set; }
            public bool Filter { get; set; }
            public bool Sort { get; set; }
        }

        public class MediatrSettings
        {
            // TODO: assembly-type name to register, empty is register this.project (Startup)
            public bool RegisterAssembly { get; set; }
        }

        public class ApiSettings
        {
            public string Version { get; set; }
            public string BaseUrl { get; set; }
        }

        public class ProjectSettings
        {
            public string Controllers { get; set; }
            public string ResourceModel { get; set; }
        }
    }
}
