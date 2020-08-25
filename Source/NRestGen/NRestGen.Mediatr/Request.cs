using MediatR;

namespace NRestGen.Mediatr
{
    public abstract class Request<ResourceT> : NRestGen.Request<ResourceT>, IRequest<Response<ResourceT>>
        where ResourceT : class
    {
        protected Request(int id)
            : base(id)
        { }
    }

    public class GetRequest<ResourceT> : Request<ResourceT>
        where ResourceT : class
    {
        public GetRequest(int id = 0)
            : base(id)
        { }
    }
}
