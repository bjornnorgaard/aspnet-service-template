﻿using Ast.Platform.Options;
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
        var options = new LoggingOptions(configuration);

        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource
                .AddService(options.ApplicationName))
            .WithMetrics(metrics => metrics
                .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                .AddMeter("Microsoft.AspNetCore.Hosting")
                .AddMeter(TelemetryMeters.Meter.Name)
                .AddMeter(options.ApplicationName)
                .AddAspNetCoreInstrumentation()
                .AddRuntimeInstrumentation()
                .AddOtlpExporter())
            .WithTracing(tracing => tracing
                .AddSource(TelemetryConfig.ProjectName)
                .AddSource(options.ApplicationName)
                .AddEntityFrameworkCoreInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter());
    }

    internal static void UsePlatformTelemetry(this IApplicationBuilder app, IConfiguration configuration)
    {
    }
}