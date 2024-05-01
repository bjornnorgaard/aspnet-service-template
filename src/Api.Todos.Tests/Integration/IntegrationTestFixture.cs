using Api.Todos.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Todos.Tests.Integration;

public class IntegrationTestFixture : IDisposable
{
    public HttpClient Client { get; set; }
    public TestServer Server { get; set; }
    public TodoContext Context { get; set; }

    /// <summary>
    /// This constructor is run once before the suite starts.
    /// </summary>
    public IntegrationTestFixture()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile(GetConfigPath())
            .Build();

        var builder = new WebHostBuilder()
            .UseEnvironment("Test")
            .UseConfiguration(config)
            .UseStartup<Startup>();

        Server = new TestServer(builder);
        Client = Server.CreateClient();

        var scope = Server.Host.Services.CreateScope();
        Context = scope.ServiceProvider.GetService<TodoContext>();
        Context.Database.EnsureCreated();
    }

    /// <summary>
    /// This Dispose() method is invoked after the entire suite has completed.
    /// </summary>
    public void Dispose()
    {
        // Also handle database cleanup, if needed.
        Client.Dispose();
        Server.Dispose();
        Context.Database.EnsureDeleted();
    }

    private static string GetConfigPath()
    {
        var type = typeof(IntegrationTestFixture);
        var assemblyLocation = type.Assembly.Location;
        var projectPath = assemblyLocation.Split("\\bin").First();
        var configPath = $"{projectPath}\\Integration\\appsettings.Test.json";
        return configPath;
    }
}
