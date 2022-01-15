using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Api.Todos.Tests.Integration;

public class IntegrationTestFixture : IDisposable
{
    public HttpClient Client { get; set; }
    public TestServer Server { get; set; }

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
    }

    /// <summary>
    /// This Dispose() method is invoked after the entire suite has completed.
    /// </summary>
    public void Dispose()
    {
        // Also handle database cleanup, if needed.
        Client.Dispose();
        Server.Dispose();
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
