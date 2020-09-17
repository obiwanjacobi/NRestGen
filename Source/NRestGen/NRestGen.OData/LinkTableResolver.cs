using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;

namespace NRestGen.OData
{
    public class LinkTableResolver : ILinkTableResolver
    {
        public IEnumerable<Link> Resolve(HttpRequest request, ODataQueryOptions options, IEnumerable<Link> links)
        {
            if (links == null || !links.Any()) { return null; }

            var ctx = new ResolveContext
            {
                BaseUrl = request.Path.Value,
                Skip = options.Skip?.Value,
                Take = options.Top?.Value
            };

            foreach (var link in links)
            {
                link.Href = ResolveLink(link, ctx);
            }

            return links
                .Where(l => l.Href != null)
                .ToList();
        }

        protected virtual Uri ResolveLink(Link link, ResolveContext ctx)
        {
            return link.Rel switch
            {
                Link.RelSelf => ResolveSelf(link, ctx),
                _ => null
            };
        }

        protected virtual Uri ResolveSelf(Link link, ResolveContext ctx)
        {
            return new Uri(ctx.BaseUrl, UriKind.Relative);
        }

        protected class ResolveContext
        {
            public string BaseUrl { get; set; }
            public int? Skip { get; set; }
            public int? Take { get; set; }
        }
    }
}
