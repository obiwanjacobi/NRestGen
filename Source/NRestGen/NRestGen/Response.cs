using System.Collections.Generic;

namespace NRestGen
{
    public class Response<ResourceT>
    {
        public Response()
        { }

        public Response(IEnumerable<ResourceT> collection)
        {
            Collection = collection;
        }

        public Response(ResourceT instance)
        {
            Collection = new[] { instance };
        }

        public IEnumerable<ResourceT> Collection { get; set; }
    }
}
