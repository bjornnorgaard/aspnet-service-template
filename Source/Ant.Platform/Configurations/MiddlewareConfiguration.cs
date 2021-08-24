using Microsoft.AspNetCore.Builder;
using Svc.Platform.Middleware;

namespace Svc.Platform.Configurations
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