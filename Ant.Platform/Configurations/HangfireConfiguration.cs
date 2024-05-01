using Ant.Platform.Hangfire;
using Ant.Platform.Options;
using Hangfire;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ant.Platform.Configurations;

public static class HangfireConfiguration
{
    public static void AddPlatformHangfire(this IServiceCollection services, IConfiguration c)
    {
        var options = new HangfireOptions(c);
        if (options.EnabledHangfire == false) return;

        services.AddHangfire(o =>
        {
            o.UseSqlServerStorage(options.ConnectionString);
            o.AddMediatR();
        });

        services.AddHangfireServer();
    }

    public static void EnabledHangfireDashboard(this IEndpointRouteBuilder endpoints, IConfiguration c)
    {
        var options = new HangfireOptions(c);
        if (options.EnabledHangfire == false) return;
        endpoints.MapHangfireDashboard();
    }
}
