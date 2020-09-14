using System.Collections.Generic;

namespace NRestGen
{
    public class LinkTableBuilder
    {
        private readonly List<Link> _links = new List<Link>();

        public LinkTableBuilder AddGetSelf()
        {
            _links.Add(NewLink(Link.ActionGet, Link.RelSelf));
            return this;
        }

        public LinkTableBuilder AddPrevPage()
        {
            _links.Add(NewLink(Link.ActionGet, Link.RelPrevPage));
            return this;
        }

        public LinkTableBuilder AddNextPage()
        {
            _links.Add(NewLink(Link.ActionGet, Link.RelNextPage));
            return this;
        }

        public LinkTableBuilder AddGet<ResourceT>()
        {
            _links.Add(NewLink(Link.ActionGet, typeof(ResourceT).Name));
            return this;
        }

        public LinkTableBuilder Add<ResourceT>(string action)
        {
            _links.Add(NewLink(action, typeof(ResourceT).Name));
            return this;
        }

        public LinkTableBuilder Add(string action, string rel)
        {
            _links.Add(NewLink(action, rel));
            return this;
        }

        public List<Link> ToList()
        {
            return new List<Link>(_links);
        }

        protected virtual Link NewLink(string action, string rel)
        {
            return new Link
            {
                Rel = rel,
                Action = action
            };
        }
    }
}
