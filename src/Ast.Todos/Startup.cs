using Ast.Platform;
using Ast.Platform.Options;
using Ast.Todos.Database;
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
        var cs = new ServiceOptions(Configuration).ConnectionString;
        services.AddDbContext<TodoContext>(o => o.UseNpgsql(cs));
        var assembly = typeof(Startup).Assembly;
        services.AddPlatformServices(Configuration, assembly);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TodoContext todoContext)
    {
        todoContext.Database.Migrate();
        app.UsePlatformServices(Configuration);
    }
}