using System.Linq;
using Ant.Todo.Api.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;

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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(t =>
                {
                    if (t.FullName.Contains("+")) return t.FullName.Split(".").Last().Replace("+", "");
                    return t.Name;
                });
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Ant.Todo.Api", Version = "v1"});
            });
            var cs = Configuration.GetConnectionString("Default");
            services.AddDbContext<TodoContext>(o => o.UseNpgsql(cs));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ant.Todo.Api v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<TodoContext>();
            context.Database.Migrate();
        }
    }
}