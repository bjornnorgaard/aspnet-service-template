using Ast.Platform;
using Ast.Platform.Options;
using Ast.Users.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddPlatformServices();
var cs = new ServiceOptions(builder.Configuration).ConnectionString;
builder.Services.AddDbContext<UserContext>(o => o.UseNpgsql(cs));

var app = builder.Build();
var context = app.Services.GetService<UserContext>();
context?.Database.Migrate();
app.MapPlatformServices();

// app.MapGet("/weatherforecast", () =>
//     {
//         var forecast = Enumerable.Range(1, 5).Select(index =>
//                 new WeatherForecast
//                 (
//                     DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//                     Random.Shared.Next(-20, 55),
//                     summaries[Random.Shared.Next(summaries.Length)]
//                 ))
//             .ToArray();
//         return forecast;
//     })
//     .WithName("GetWeatherForecast")
//     .WithOpenApi();

app.Run();
