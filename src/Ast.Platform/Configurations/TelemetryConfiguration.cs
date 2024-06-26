﻿using Ast.Platform.Options;
using Ast.Platform.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Ast.Platform.Configurations;

internal static class TelemetryConfiguration
{
    internal static void AddPlatformTelemetry(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        var options = new ServiceOptions(configuration);
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "null";
        var attributes = new Dictionary<string, object>
        {
            ["environment.name"] = env,
            ["server.name"] = Environment.MachineName,
        };

        var resourceBuilder = ResourceBuilder
            .CreateDefault()
            .AddAttributes(attributes)
            .AddService(serviceName: options.ServiceName);

        builder.Logging.AddOpenTelemetry(o =>
        {
            o.IncludeFormattedMessage = true;
            o.IncludeScopes = true;
            o.SetResourceBuilder(resourceBuilder);
            o.AddOtlpExporter();
        });

        builder.Services.AddPlatformTelemetry(configuration);
    }

    internal static void AddPlatformTelemetry(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new ServiceOptions(configuration);
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "null";
        var attributes = new Dictionary<string, object>
        {
            ["environment.name"] = env,
            ["server.name"] = Environment.MachineName,
        };

        var resourceBuilder = ResourceBuilder
            .CreateDefault()
            .AddAttributes(attributes)
            .AddService(serviceName: options.ServiceName);

        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource
                .AddService(options.ServiceName)
                .AddAttributes(attributes))
            .WithTracing(tracing => tracing
                .SetResourceBuilder(resourceBuilder)
                .AddSource(TelemetrySource.ProjectName)
                .AddSource(options.ServiceName)
                .AddEntityFrameworkCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter(b => b.Endpoint = new Uri(options.TelemetryCollectorHost)))
            .WithMetrics(metrics => metrics
                .SetResourceBuilder(resourceBuilder)
                .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                .AddMeter("Microsoft.AspNetCore.Hosting")
                .AddMeter("Microsoft.AspNetCore.Http.Connections")
                .AddMeter("Microsoft.AspNetCore.Routing")
                .AddMeter("Microsoft.AspNetCore.Diagnostics")
                .AddMeter("Microsoft.AspNetCore.RateLimiting")
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddProcessInstrumentation()
                .AddOtlpExporter(b => b.Endpoint = new Uri(options.TelemetryCollectorHost)));
    }

    internal static void UsePlatformTelemetry(this IApplicationBuilder app, IConfiguration configuration)
    {
    }
}
