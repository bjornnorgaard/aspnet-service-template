using System.Text.Json.Serialization;
using Ast.Platform.Configurations;
using Ast.Platform.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ast.Platform;

public static class ServiceCollectionExtension
{
    public static void AddPlatformServices(this IServiceCollection services, IConfiguration configuration)
    {
        configuration.ValidatePlatformConfiguration();
        services.AddPlatformTelemetry(configuration);
        services.AddCorsPolicy(configuration);
        services.AddControllers(o => o.Filters.Add<ExceptionFilter>()).AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        services.AddHealthChecks();
        services.AddPlatformMediatr();
        services.AddPlatformSwagger(configuration);
        services.AddPlatformHangfire(configuration);
    }

    public static WebApplicationBuilder AddPlatformServices(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        configuration.ValidatePlatformConfiguration();
        builder.AddPlatformTelemetry(configuration);
        builder.Services.AddPlatformMediatr();
        builder.Services.AddPlatformHangfire(configuration);
        builder.Services.AddCorsPolicy(configuration);
        builder.Services.AddHealthChecks();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.CustomSchemaIds(t =>
            {
                if (string.IsNullOrWhiteSpace(t.FullName)) return t.Name;
                if (t.FullName.Contains('+')) return t.FullName.Split(".").Last().Replace("+", "");
                return t.Name;
            });
        });

        return builder;
    }

    public static void UsePlatformServices(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UsePlatformTelemetry(configuration);
        app.UsePlatformSwagger(configuration);
        app.UseRouting();
        app.UseCorsPolicy();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/hc");
            endpoints.EnabledHangfireDashboard(configuration);
        });
    }

    public static WebApplication MapPlatformServices(this WebApplication app)
    {
        var configuration = app.Configuration;
        app.UsePlatformTelemetry(configuration);
        app.UseCorsPolicy();
        app.MapHealthChecks("/hc");
        app.EnabledHangfireDashboard(configuration);
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
}
