using Microsoft.Extensions.Hosting;
using Serilog;

namespace Ast.Platform;

public static class HostExtensions
{
    public static IHostBuilder UsePlatformLogger(this IHostBuilder builder)
    {
        builder.UseSerilog();

        return builder;
    }
}