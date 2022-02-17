using Ant.Platform;
using Api.Todos.Database;
using Api.Todos.Options;
using Microsoft.EntityFrameworkCore;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var assembly = typeof(Startup).Assembly;

        var connectionString = new DatabaseOptions(Configuration).TodoDatabase;
        services.AddDbContext<TodoContext>(o => o.UseSqlServer(connectionString));

        services.AddAutoMapper(assembly);
        services.AddPlatformServices(Configuration, assembly);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TodoContext todoContext)
    {
        todoContext.Database.Migrate();
        app.UsePlatformServices(Configuration);
    }
}