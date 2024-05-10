using Ast.Platform;

namespace Ast.Todos;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host
            .CreateDefaultBuilder(args)
            .UsePlatformLogger()
            .ConfigureWebHostDefaults(c => c.UseStartup<Startup>());
    }
}