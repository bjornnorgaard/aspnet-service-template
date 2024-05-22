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
            throw new Exception($"Env var {key} was empty.");
        }

        return builder.ConfigureLogging(loggingBuilder =>
        {
            loggingBuilder.AddOpenTelemetry(loggerOptions =>
                {
                    loggerOptions.IncludeScopes = true;
                    loggerOptions.IncludeFormattedMessage = true;
                    loggerOptions.AddOtlpExporter(options => options.Endpoint = new Uri(collectorEndpoint));
                }
            );
        });
    }
}
