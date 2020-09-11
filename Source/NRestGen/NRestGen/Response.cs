using System.Collections.Generic;

namespace NRestGen
{
    public class Response<ResourceT>
    {
        public Response(IEnumerable<ResourceT> collection, int? count = null)
        {
            Results = collection;
            Count = count;
        }

        public Response(ResourceT instance)
        {
            Results = new[] { instance };
        }

        public IEnumerable<ResourceT> Results { get; set; }

        public IEnumerable<Link> Links { get; set; }

        public int? Count { get; set; }
    }
}
