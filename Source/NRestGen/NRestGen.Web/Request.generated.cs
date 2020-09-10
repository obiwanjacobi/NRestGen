// This code is generated by NRestGen v1.0.0.
// Any changes to this file will be overwritten when regenerated.
// Generated at 2020-09-10 11:14:50
using System;
using MediatR;

namespace NRestGen.Web
{
    public abstract partial class Request<ResourceT> : NRestGen.Request<ResourceT>
            , IRequest<NRestGen.Response<ResourceT>>
        where ResourceT : class
    {
        public Request(int id = 0) : base(id) { }
    }

    public partial class GetRequest<ResourceT> : Request<ResourceT> where ResourceT : class
    {
        public GetRequest(int id = 0) : base(id) { }
    }

    public partial class PutRequest<ResourceT> : Request<ResourceT> where ResourceT : class
    {
        public PutRequest(int id = 0) : base(id) { }
    }

    public partial class PatchRequest<ResourceT> : Request<ResourceT> where ResourceT : class
    {
        public PatchRequest(int id = 0) : base(id) { }
    }

    public partial class DeleteRequest<ResourceT> : Request<ResourceT> where ResourceT : class
    {
        public DeleteRequest(int id = 0) : base(id) { }
    }
}
