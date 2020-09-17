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
    }
}
