using Ast.Platform.Options;
using Ast.Platform.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Ast.Platform.Configurations;

internal static class TelemetryConfiguration
{
    internal static void AddPlatformTelemetry(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new ServiceOptions(configuration);

        services.AddOpenTelemetry()
            .ConfigureResource(builder => builder
                .AddService(serviceName: options.ServiceName, serviceInstanceId: Environment.MachineName))
            .WithTracing(tracing => tracing
                .AddSource(TelemetrySource.ProjectName)
                .AddSource(options.ServiceName)
                .AddEntityFrameworkCoreInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter())
            .WithMetrics(metrics => metrics
                .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                .AddMeter("Microsoft.AspNetCore.Hosting")
                .AddMeter(TelemetryMeters.Meter.Name)
                .AddAspNetCoreInstrumentation()
                .AddRuntimeInstrumentation()
                .AddOtlpExporter());
    }

    internal static void UsePlatformTelemetry(this IApplicationBuilder app, IConfiguration configuration)
    {
    }
}