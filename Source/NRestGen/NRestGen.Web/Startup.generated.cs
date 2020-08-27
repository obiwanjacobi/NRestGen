// This code is generated by NRestGen v1.0.0.
// Any changes to this file will be overwritten when regenerated.
// Generated at 2020-08-27 15:40:22
using Microsoft.AspNet.OData.Extensions;
using NRestGen.Web.ResourceModel;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace NRestGen.Web
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddNRestGen(this IServiceCollection services)
        {
            services.AddOData();
            return services;
        }

        public static IEndpointRouteBuilder AddNRestGen(this IEndpointRouteBuilder endpoints)
        {
            var resourceModel = ResourceModelBuilder.Build();
            endpoints.MapODataRoute("OData", "api", resourceModel);
            endpoints.EnableDependencyInjection();
            endpoints
                .Select()
                .Filter()
                .Count()
                .MaxTop(100)
                ;
            return endpoints;
        }
    }
}
