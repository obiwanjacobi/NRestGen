// This code is generated by NRestGen v1.0.0.
// Any changes to this file will be overwritten when regenerated.
// Generated at 2020-08-27 14:29:52
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

namespace NRestGen.Web.ResourceModel
{
    public static partial class ResourceModelBuilder
    {
        public static IEdmModel Build()
        {
            var builder = new ODataConventionModelBuilder
            {
                Namespace = "NRestGen.Web.ResourceModel"
            };
            builder.EnableLowerCamelCase();

            builder.EntitySet<Customer>("Customers");
            builder.EntitySet<Order>("Orders");
            builder.EntitySet<OrderLine>("OrderLines");

            return builder.GetEdmModel();
        }
    }
}
