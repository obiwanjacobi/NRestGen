using System;
using Microsoft.AspNet.OData.Query;

namespace NRestGen.OData
{
    public class ODataRequest<ResourceT> : Request<ResourceT>
        where ResourceT : class
    {
        public ODataRequest(ODataQueryOptions<ResourceT> options, int id = 0)
            : base(id)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public ODataQueryOptions<ResourceT> Options { get; private set; }
    }
}
