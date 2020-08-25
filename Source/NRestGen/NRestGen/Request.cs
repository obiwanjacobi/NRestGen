namespace NRestGen
{
    public class Request<ResourceT>
        where ResourceT : class
    {
        protected Request(int id)
        {
            Identifier = new ResourceIdentifier(typeof(ResourceT), id);
        }

        public ResourceIdentifier Identifier { get; }

        public ResourceIdentifier ParentId { get; set; }
    }
}
