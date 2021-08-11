using Ant.Platform;
using Ant.Todo.Api.Database;
using Ant.Todo.Api.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ant.Todo.Api
{
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
}