// This code is generated by NRestGen v1.0.0.
// Any changes to this file will be overwritten when regenerated.
// Generated at 2020-09-10 12:51:04
using System;
using MediatR;
using Microsoft.AspNet.OData.Query;

namespace NRestGen.Web
{
    public abstract partial class Request<ResourceT> : NRestGen.Request<ResourceT>
            , IRequest<NRestGen.Response<ResourceT>>
        where ResourceT : class
    {
        public Request(ODataQueryOptions<ResourceT> options, int id = 0)
            : base(id)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public ODataQueryOptions<ResourceT> Options { get; private set; }
    }

    public partial class GetRequest<ResourceT> : Request<ResourceT> where ResourceT : class
    {
        public GetRequest(ODataQueryOptions<ResourceT> options, int id = 0) : base(options, id) { }
    }

    public partial class PutRequest<ResourceT> : Request<ResourceT> where ResourceT : class
    {
        public PutRequest(ODataQueryOptions<ResourceT> options, int id = 0) : base(options, id) { }
    }

    public partial class PatchRequest<ResourceT> : Request<ResourceT> where ResourceT : class
    {
        public PatchRequest(ODataQueryOptions<ResourceT> options, int id = 0) : base(options, id) { }
    }

    public partial class DeleteRequest<ResourceT> : Request<ResourceT> where ResourceT : class
    {
        public DeleteRequest(ODataQueryOptions<ResourceT> options, int id = 0) : base(options, id) { }
    }
}
