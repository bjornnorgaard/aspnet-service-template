using Ast.Platform;
using Ast.Platform.Options;
using Ast.Todos.Database;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Ast.Todos;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var cs = new ServiceOptions(Configuration).ConnectionString;
        services.AddDbContext<TodoContext>(o => o.UseNpgsql(cs));
        var assembly = typeof(Startup).Assembly;
        services.AddPlatformServices(Configuration, assembly);
    }

    public void Configure(IApplicationBuilder app, TodoContext todoContext, ILogger<Startup> logger)
    {
        try
        {
            todoContext.Database.Migrate();
        }
        catch (PostgresException e)
        {
            logger.LogError(e, "Failed to migrate database");
            Console.WriteLine(e);
        }

        app.UsePlatformServices(Configuration);
    }
}
