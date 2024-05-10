using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;

namespace Ast.Platform;

public static class HostExtensions
{
    public static IHostBuilder UsePlatformLogger(this IHostBuilder builder)
    {
        return builder.ConfigureLogging(loggingBuilder =>
        {
            loggingBuilder.AddOpenTelemetry(loggerOptions =>
                {
                    loggerOptions.IncludeScopes = true;
                    loggerOptions.IncludeFormattedMessage = true;
                    loggerOptions.AddOtlpExporter();
                }
            );
        });
    }
}