using System.Collections.Generic;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;

namespace NRestGen.OData
{
    public interface ILinkTableResolver
    {
        IEnumerable<Link> Resolve(HttpRequest request, ODataQueryOptions options, IEnumerable<Link> links);
    }
}
