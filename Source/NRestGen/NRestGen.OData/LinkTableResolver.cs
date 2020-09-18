using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                QueryString = "",       // TODO: get query string from options
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
                Link.RelNextPage => ResolveNextPage(link, ctx),
                Link.RelPrevPage => ResolvePrevPage(link, ctx),
                _ => null
            };
        }

        protected virtual Uri ResolveNextPage(Link link, ResolveContext ctx)
        {
            if (ctx.Take != null)
            {
                var skip = ctx.Skip.GetValueOrDefault() + ctx.Take.Value;
                return CreateUri(ctx.BaseUrl, $"$skip={skip}", $"$top={ctx.Take.Value}");
            }
            return null;
        }

        protected virtual Uri ResolvePrevPage(Link link, ResolveContext ctx)
        {
            if (ctx.Take != null &&
                ctx.Skip.GetValueOrDefault() > 0)
            {
                var skip = ctx.Skip.GetValueOrDefault() - ctx.Take.Value;
                return CreateUri(ctx.BaseUrl, $"$skip={skip}", $"$top={ctx.Take.Value}");
            }
            return null;
        }

        protected virtual Uri ResolveSelf(Link link, ResolveContext ctx)
        {
            return CreateUri(ctx.BaseUrl);
        }

        private Uri CreateUri(string baseUrl, params string[] queries)
        {
            var query = new StringBuilder();

            foreach (var q in queries)
            {
                if (query.Length > 0)
                {
                    query.Append("&");
                }

                query.Append(q);
            }

            string path;
            if (query.Length > 0) { path = $"{baseUrl}?{query}"; }
            else { path = baseUrl; }

            return new Uri(path, UriKind.Relative);
        }

        protected class ResolveContext
        {
            public string BaseUrl { get; set; }
            public string QueryString { get; set; }
            public int? Skip { get; set; }
            public int? Take { get; set; }
        }
    }
}
