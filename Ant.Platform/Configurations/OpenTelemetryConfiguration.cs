using Ant.Platform.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Ant.Platform.Configurations;

public static class OpenTelemetryConfiguration
{
    public static void AddPlatformTelemetry(this IServiceCollection services, IConfiguration c)
    {
        var opts = new OpenTelemetryOptions(c);

        var telemetryBuilder = services.AddOpenTelemetry();

        telemetryBuilder.ConfigureResource(r => r.AddService(opts.ApplicationName))
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter())
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddOtlpExporter()
                .AddPrometheusExporter());
    }

    public static void UsePlatformTelemetry(this IApplicationBuilder app)
    {
        app.UseOpenTelemetryPrometheusScrapingEndpoint();
    }
}