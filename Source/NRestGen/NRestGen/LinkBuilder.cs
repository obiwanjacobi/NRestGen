using System;

namespace NRestGen
{
    public sealed class LinkBuilder
    {
        private readonly string _path;
        private readonly string _query;

        public LinkBuilder(string path, string query)
        {
            _path = path;
            _query = query;
        }

        public Link Self()
        {
            var path = $"{_path}{_query}";
            return new Link
            {
                Action = Link.ActionGet,
                Rel = Link.RelSelf,
                Href = new Uri(path, UriKind.Relative)
            };
        }

        public Link NextPage(int currentSkip, int currentTake)
        {
            var path = _path + $"?$skip={currentSkip + currentTake}&$top={currentTake}";

            return new Link
            {
                Action = Link.ActionGet,
                Rel = Link.RelNextPage,
                Href = new Uri(path, UriKind.Relative)
            };
        }

        public Link PrevPage(int currentSkip, int currentTake)
        {
            var path = _path;
            if (currentSkip > currentTake)
            {
                path += $"?$skip={currentSkip - currentTake}&$top={currentTake}";
            }
            else
            {
                path += $"?$skip=0&$top={currentTake}";
            }

            return new Link
            {
                Action = Link.ActionGet,
                Rel = Link.RelPrevPage,
                Href = new Uri(path, UriKind.Relative)
            };
        }
    }
}
