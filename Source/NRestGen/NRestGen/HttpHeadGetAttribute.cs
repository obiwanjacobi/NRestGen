using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;

namespace NRestGen
{
    public sealed class HttpHeadGetAttribute : HttpMethodAttribute
    {
        private static readonly IEnumerable<string> _supportedMethods = new[] { "GET", "HEAD" };

        public HttpHeadGetAttribute()
            : base(_supportedMethods)
        { }

        public HttpHeadGetAttribute(string template)
            : base(_supportedMethods, template)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }
        }
    }
}
