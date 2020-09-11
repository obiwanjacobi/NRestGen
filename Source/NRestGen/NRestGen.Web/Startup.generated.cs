// This code is generated by NRestGen v1.0.0.
// Any changes to this file will be overwritten when regenerated.
// Generated at 2020-09-11 13:47:00
using MediatR;
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
            services.AddMediatR(typeof(Startup).Assembly);
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
