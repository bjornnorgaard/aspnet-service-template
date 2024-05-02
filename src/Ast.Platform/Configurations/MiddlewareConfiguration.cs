using AST.Platform.Middleware;
using Microsoft.AspNetCore.Builder;

namespace AST.Platform.Configurations;

public static class MiddlewareConfiguration
{
    public static void UsePlatformMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<CorrelationMiddleware>();
        app.UseMiddleware<LoggingMiddleware>();
    }
}