using Ant.Platform;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UsePlatformLogger();
builder.Host.ConfigureWebHostDefaults(host => host.UseStartup<Startup>());
var app = builder.Build();
app.Run();
