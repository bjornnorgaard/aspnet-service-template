using System;
using Ant.Platform.Options;
using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Ant.Platform.Configurations
{
    public static class LoggingConfiguration
    {
        public static void AddPlatformLogging(this IServiceCollection services, IConfiguration configuration)
        {
            Console.WriteLine("Setting up platform logging...");

            var options = new LoggingOptions(configuration);

            var esOptions = new ElasticsearchSinkOptions(new Uri(options.ElasticSearchUrl))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"logstash-{DateTime.Now:yyyy.MM.dd}",
                ModifyConnectionSettings = x => x.BasicAuthentication("elastic", "changeme")
            };

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithProperty("Application", options.ApplicationName)
                .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                .WriteTo.Console()
                .WriteTo.Elasticsearch(esOptions)
                .CreateLogger();

            Log.Information("Logger configured!");
        }

        public static void UsePlatformLogging(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseAllElasticApm(configuration);
        }
    }
}