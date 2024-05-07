using Ast.Platform;
using Ast.Todos.Database;
using Ast.Todos.Options;
using Microsoft.EntityFrameworkCore;

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
        var connectionString = new DatabaseOptions(Configuration).TodoDatabase;
        services.AddDbContext<TodoContext>(o => o.UseSqlServer(connectionString));

        var assembly = typeof(Startup).Assembly;
        services.AddPlatformServices(Configuration, assembly);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TodoContext todoContext)
    {
        todoContext.Database.Migrate();
        app.UsePlatformServices(Configuration);
    }
}