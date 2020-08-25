using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;

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

            // >>>> setup NRestGen services
            services.AddNRestGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // >>>> Setup NRestGen endpoints
                endpoints.AddNRestGen();
            });
        }
    }
}
