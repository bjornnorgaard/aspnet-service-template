using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ant.Platform.Configurations;

public static class CorsConfiguration
{
    private const string DefaultPolicy = "DefaultCorsPolicy";

    public static void AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var asString = configuration.GetValue<string>("AllowedOrigins");
        var origins = asString.Split(",");

        services.AddCors(options =>
        {
            options.AddPolicy(DefaultPolicy, builder =>
            {
                builder.WithOrigins(origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }

    public static void UseCorsPolicy(this IApplicationBuilder app)
    {
        app.UseCors(DefaultPolicy);
    }
}
