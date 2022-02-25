using System.Reflection;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using SwaggerOptions = Ant.Platform.Options.SwaggerOptions;

namespace Ant.Platform.Configurations;

internal static class SwaggerConfiguration
{
    internal static void AddPlatformSwagger(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly assembly)
    {
        var options = new SwaggerOptions(configuration);

        services.AddFluentValidation(o => o.RegisterValidatorsFromAssembly(assembly));
        services.AddFluentValidationRulesToSwagger();

        services.AddSwaggerGen(c =>
        {
            c.CustomSchemaIds(t =>
            {
                if (string.IsNullOrWhiteSpace(t.FullName))
                {
                    return t.Name;
                }

                if (t.FullName.Contains('+'))
                {
                    return t.FullName.Split(".").Last().Replace("+", "");
                }

                return t.Name;
            });
            c.SwaggerDoc("v1", new OpenApiInfo { Title = options.ApplicationTitle, Version = "v1" });
            c.AddFluentValidationRulesScoped();
        });
    }

    internal static void UsePlatformSwagger(this IApplicationBuilder app, IConfiguration configuration)
    {
        var options = new SwaggerOptions(configuration);

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            // OpenAPI URL: http://localhost:port/swagger/v1/swagger.json
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{options.ApplicationTitle} v1");
        });
    }
}
