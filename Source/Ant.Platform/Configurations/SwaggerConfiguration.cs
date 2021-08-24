using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Svc.Platform.Options;

namespace Svc.Platform.Configurations
{
    internal static class SwaggerConfiguration
    {
        internal static void AddPlatformSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new SwaggerOptions(configuration);

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(t =>
                {
                    if (t.FullName.Contains("+"))
                    {
                        return t.FullName.Split(".").Last().Replace("+", "");
                    }

                    return t.Name;
                });
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = options.ApplicationTitle, Version = "v1",
                });
            });
        }

        internal static void UsePlatformSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var options = new SwaggerOptions(configuration);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{options.ApplicationTitle} v1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}