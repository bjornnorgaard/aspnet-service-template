using Ant.Platform;
using Api.Todos;
using Api.Todos.Database;
using Api.Todos.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UsePlatformLogger();
var configuration = builder.Configuration;
var services = builder.Services;

// Dependency Injection
var assembly = typeof(AssemblyAnchor).Assembly;

var connectionString = new DatabaseOptions(configuration).TodoDatabase;
services.AddDbContext<TodoContext>(o => o.UseSqlServer(connectionString));

services.AddAutoMapper(assembly);
services.AddPlatformServices(configuration, assembly);

// Middleware
var app = builder.Build();
var scope = app.Services.CreateScope();
var todoContext = scope.ServiceProvider.GetService<TodoContext>();

todoContext.Database.Migrate();
app.UsePlatformServices(configuration);

app.Run();