﻿using System;

namespace NRestGen
{
    public class Link
    {
        public const string RelSelf = "self";
        public const string RelNextPage = "nextPage";
        public const string RelPrevPage = "prevPage";

        // actions
        public const string ActionGet = "GET";
        public const string ActionPut = "PUT";
        public const string ActionPost = "POST";
        public const string ActionPatch = "PATCH";
        public const string ActionDelete = "DELETE";
        public const string ActionHead = "HEAD";
        public const string ActionOptions = "OPTIONS";

        /// <summary>Resource Entity name or 'self'.</summary>
        public string Rel { get; set; }

        /// <summary>Http Verb</summary>
        public string Action { get; set; }

        /// <summary>Url</summary>
        public Uri Href { get; set; }
    }
}
