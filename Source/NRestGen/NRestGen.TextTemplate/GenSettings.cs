﻿namespace NRestGen.TextTemplate
{
    public sealed class GenSettings
    {
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
            public bool RegisterAssembly { get; set; }
        }
    }
}
