using Microsoft.AspNetCore.Builder;
using Ant.Platform.Middleware;

namespace Ant.Platform.Configurations
{
    public static class MiddlewareConfiguration
    {
        public static void UsePlatformMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CorrelationMiddleware>();
            app.UseMiddleware<LoggingMiddleware>();
        }
    }
}