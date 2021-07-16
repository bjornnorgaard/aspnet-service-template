using Ant.Platform.Hangfire;
using Ant.Platform.Options;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ant.Platform.Configurations
{
    public static class HangfireConfiguration
    {
        public static void AddPlatformHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new HangfireOptions(configuration);
            
            services.AddHangfire(o =>
            {
                o.UsePostgreSqlStorage(options.ConnectionString);
                o.AddMediatR();
            });

            services.AddHangfireServer();
        }

        public static void EnabledHangfireDashboard(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHangfireDashboard();
        }
    }
}