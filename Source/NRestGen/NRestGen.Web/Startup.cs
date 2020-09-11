﻿using System;
using System.Linq;
using System.Xml;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OpenApi.Models;
using NRestGen.Web.ResourceModel;

namespace NRestGen.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // >>>> the resource model
        public IEdmModel ResourceModel { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddApiVersioning(options =>
            //{
            //    options.ReportApiVersions = true;
            //    options.DefaultApiVersion = new ApiVersion(1, 0);
            //    //options.AssumeDefaultVersionWhenUnspecified = true;
            //});
            //services.AddVersionedApiExplorer(options =>
            //{
            //    options.GroupNameFormat = "'v'VVV";
            //    options.SubstituteApiVersionInUrl = true;
            //});

            // >>>> setup NRestGen services
            services.AddNRestGen();
            AddODataFormatters(services);

            // >>>> Set JSON serialization options
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
#if DEBUG
                    options.JsonSerializerOptions.WriteIndented = true;
#endif
                });

            // Register the Swagger Generator service. This service is responsible for genrating Swagger Documents.
            // Note: Add this service at the end after AddMvc() or AddMvcCore().
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "NRestGen Demo API",
                    Version = "v1",
                    Description = "This API is generated by NRestGen.",
                    Contact = new OpenApiContact
                    {
                        Name = "Marc Jacobi",
                        Email = "obiwanjacobi@hotmail.com",
                        Url = new Uri("https://github.com/obiwanjacobi/NRestGen"),
                    },
                });
            });

#if DEBUG
            var resourceModel = ResourceModelBuilder.Build();
            using var writer = XmlWriter.Create("ResourceModel.xml");
            CsdlWriter.TryWriteCsdl(resourceModel, writer, CsdlTarget.OData, out var errors);
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env
            /*, IApiVersionDescriptionProvider provider*/)
        {
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseRouteDebugger();
            }

            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            // TODO:
            //app.UseODataBatching();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // >>>> Setup NRestGen endpoints
                endpoints.AddNRestGen();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NRestGen Demo API v1.0");
                //foreach (var description in provider.ApiVersionDescriptions)
                //{
                //    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                //        "NRestGen Demo API " + description.GroupName.ToUpperInvariant());
                //}
            });
        }

        private void AddODataFormatters(IServiceCollection services)
        {
            services.AddMvcCore(options =>
            {
                foreach (var formatter in options.OutputFormatters
                    .OfType<ODataOutputFormatter>()
                    .Where(it => !it.SupportedMediaTypes.Any()))
                {
                    formatter.SupportedMediaTypes.Add(
                        new MediaTypeHeaderValue("application/prs.mock-odata"));
                }

                foreach (var formatter in options.InputFormatters
                    .OfType<ODataInputFormatter>()
                    .Where(it => !it.SupportedMediaTypes.Any()))
                {
                    formatter.SupportedMediaTypes.Add(
                        new MediaTypeHeaderValue("application/prs.mock-odata"));
                }
            });
        }
    }
}
