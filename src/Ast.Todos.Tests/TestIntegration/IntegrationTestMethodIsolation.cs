using Ast.Todos.Database;
using Ast.Todos.Options;
using AST.Platform.Options;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Testcontainers.MsSql;
using Xunit;

namespace Ast.Todos.Tests.TestIntegration;

[CollectionDefinition(nameof(IntegrationTestCollection))]
public class IntegrationTestCollection : ICollectionFixture<IntegrationTestMethodIsolation>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

public class IntegrationTestMethodIsolation : IAsyncLifetime
{
    public HttpClient Client { get; set; }
    public TodoContext Context { get; set; }
    private TestServer Server { get; set; }
    public IMapper Mapper { get; set; }

    private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    /// <summary>
    /// Run once before the suite starts.
    /// </summary>
    public async Task InitializeAsync()
    {
        await _sqlContainer.StartAsync();

        var config = new ConfigurationBuilder()
            .AddJsonFile(GetConfigPath())
            .Build();

        var cs = _sqlContainer.GetConnectionString();
        config[$"{nameof(DatabaseOptions)}:{nameof(DatabaseOptions.TodoDatabase)}"] = cs;
        config[$"{nameof(HangfireOptions)}:{nameof(HangfireOptions.ConnectionString)}"] = cs;

        var builder = new WebHostBuilder()
            .UseEnvironment("Test")
            .UseConfiguration(config)
            .UseStartup<Startup>();

        Mapper = new MapperConfiguration(c => c.AddMaps(typeof(AssemblyAnchor).Assembly)).CreateMapper();

        Server = new TestServer(builder);
        Client = Server.CreateClient();
        Context = new TodoContext(new DbContextOptionsBuilder<TodoContext>().UseSqlServer(cs).Options);
        await Context.Database.EnsureCreatedAsync();
    }

    /// <summary>
    /// Invoked after the entire suite has completed.
    /// </summary>
    public async Task DisposeAsync()
    {
        Client.Dispose();
        Server.Dispose();
        await _sqlContainer.DisposeAsync();
    }

    private static string GetConfigPath()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var split = currentDirectory.Split("bin");
        var testProject = split[0];
        var settingPath = Path.Combine(testProject, "TestIntegration", "appsettings.Test.json");
        return settingPath;
    }
}
