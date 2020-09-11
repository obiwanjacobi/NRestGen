namespace NRestGen
{
    public class Request<ResourceT>
        where ResourceT : class
    {
        public Request(int id = 0)
        {
            Identifier = new ResourceIdentifier(typeof(ResourceT), id);
        }

        public ResourceIdentifier Identifier { get; }

        public ResourceIdentifier ParentId { get; set; }

        public RequestContext Context { get; set; }
    }

    public class RequestContext
    {
        public RequestContext(string method, string path, string query)
        {
            Method = method;
            Path = path;
            Query = query;
        }

        public string Path { get; private set; }
        public string Query { get; private set; }
        public string Method { get; private set; }
    }
}
