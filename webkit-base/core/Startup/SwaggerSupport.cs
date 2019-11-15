using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace WebkitBase.Core.Startup
{
    public static class SwaggerSupport
    {
        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                var version = configuration.GetSection("General")["SiteVersion"];
                c.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = configuration.GetSection("General")["SiteName"],
                    Version = version,
                    Description = configuration.GetSection("General")["Description"]
                });
            });
        }

        public static void UseSwaggerWithUI(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{configuration.GetSection("General")["SiteVersion"]}/swagger.json",
                                  configuration.GetSection("General")["SiteName"]);
                c.RoutePrefix = configuration.GetSection("Swagger")["RoutePrefix"];
            });
        }
    }
}