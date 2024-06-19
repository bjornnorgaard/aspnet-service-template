using Ast.Platform.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;

namespace Ast.Platform;

public static class HostExtensions
{
    public static IHostBuilder UsePlatformLogger(this IHostBuilder builder)
    {
        const string key = $"{nameof(ServiceOptions)}__{nameof(ServiceOptions.TelemetryCollectorHost)}";
        var collectorEndpoint = Environment.GetEnvironmentVariable(key);
        if (string.IsNullOrWhiteSpace(collectorEndpoint))
        {
            collectorEndpoint = "http://localhost:18889";
            Console.WriteLine(
                $"Environment variable {key} is not set. Using default OpenTelemetry collector endpoint: {collectorEndpoint}");
        }

        return builder.ConfigureLogging(loggingBuilder =>
        {
            loggingBuilder.AddOpenTelemetry(loggerOptions =>
                {
                    loggerOptions.IncludeScopes = true;
                    loggerOptions.IncludeFormattedMessage = true;

                    if (string.IsNullOrWhiteSpace(collectorEndpoint))
                    {
                        Console.WriteLine("Using default OpenTelemetry collector endpoint.");
                        loggerOptions.AddOtlpExporter();
                    }
                    else
                    {
                        Console.WriteLine($"Using custom OpenTelemetry collector endpoint: {collectorEndpoint}");
                        loggerOptions.AddOtlpExporter(options => options.Endpoint = new Uri(collectorEndpoint));
                    }
                }
            );
        });
    }
}