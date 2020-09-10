using System.Collections.Generic;

namespace NRestGen
{
    public class Response<ResourceT>
    {
        public Response(IEnumerable<ResourceT> collection)
        {
            Results = collection;
        }

        public Response(ResourceT instance)
        {
            Results = new[] { instance };
        }

        public IEnumerable<ResourceT> Results { get; set; }
    }
}
