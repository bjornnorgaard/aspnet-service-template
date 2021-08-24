using Ant.Platform;
using Ant.Todos.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Svc.Todos.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UsePlatformLogger()
                .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>());
    }
}