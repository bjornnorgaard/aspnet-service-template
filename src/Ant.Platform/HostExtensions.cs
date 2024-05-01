using Microsoft.Extensions.Hosting;
using Serilog;

namespace Ant.Platform;

public static class HostExtensions
{
    public static IHostBuilder UsePlatformLogger(this IHostBuilder builder)
    {
        builder.UseSerilog();

        return builder;
    }
}
