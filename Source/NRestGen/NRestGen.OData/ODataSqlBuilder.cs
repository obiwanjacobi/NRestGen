using System.Linq;
using Microsoft.AspNet.OData.Query;

namespace NRestGen.OData
{
    /// <summary>
    /// Walks the ODataQueryOptions and builds a SQL string for them.
    /// </summary>
    /// <remarks>
    /// Allow name mapping of ResourceModel entities/properties
    /// </remarks>
    public class ODataSqlBuilder
    {
        public ODataSqlBuilder(ILookup<string, string> nameLookup)
        { }

        public string BuildFilter(FilterQueryOption filter)
        { return null; }

        public string BuildExpand(SelectExpandQueryOption expand)
        { return null; }

        public string BuildSelect(SelectExpandQueryOption select)
        { return null; }
    }
}
