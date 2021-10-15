using System.Reflection;
using System.Text.Json.Serialization;
using Ant.Platform.Configurations;
using Ant.Platform.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Ant.Platform
{
    public static class ServiceCollectionExtension
    {
        public static void AddPlatformServices(
            this IServiceCollection services,
            IConfiguration configuration,
            Assembly assembly)
        {
            services.AddPlatformLogging(configuration);

            Log.Information("Adding platform services...");

            configuration.ValidatePlatformConfiguration();

            services.AddMemoryCache();
            services.AddCorsPolicy(configuration);
            services.AddControllers(o => o.Filters.Add<ExceptionFilter>()).AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddHealthChecks();
            services.AddPlatformMediatr(assembly);
            services.AddPlatformSwagger(configuration);
            services.AddPlatformHangfire(configuration);

            Log.Information("Platform services added");
        }

        public static void UsePlatformServices(this IApplicationBuilder app, IConfiguration configuration)
        {
            Log.Information("Setting up platform pipeline...");

            app.UsePlatformLogging(configuration);
            app.UsePlatformSwagger(configuration);
            app.UsePlatformMiddleware();
            app.UseRouting();
            app.UseCorsPolicy();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc");
                endpoints.EnabledHangfireDashboard();
            });

            Log.Information("Platform successfully started");
        }
    }
}
