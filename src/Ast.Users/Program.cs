using Ast.Platform;
using Ast.Platform.Options;
using Ast.Users.Database;
using Ast.Users.Features.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);
builder.AddPlatformServices();
var cs = new ServiceOptions(builder.Configuration).ConnectionString;
builder.Services.AddDbContext<UserContext>(o => o.UseNpgsql(cs));

var app = builder.Build();
var context = app.Services.GetService<IMigrator>();
context?.Migrate();
app.MapPlatformServices();

app.MapUsersEndpoints();

app.Run();

internal static class UserEndpoints
{
    internal static void MapUsersEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("users");
        group.MapPost("get-users", GetUsers);
        group.MapPost("get-user", GetUser);
        group.MapPost("create-user", CreateUser);
        group.MapPost("update-user", UpdateUser);
        group.MapPost("delete-user", DeleteUser);
    }

    private static async Task<GetUser.Result> GetUser(IMediator mediator, GetUser.Command command)
    {
        return await mediator.Send(command);
    }

    private static async Task<GetUsers.Result> GetUsers(IMediator mediator, GetUsers.Command command)
    {
        return await mediator.Send(command);
    }

    private static async Task<CreateUser.Result> CreateUser(IMediator mediator, CreateUser.Command command)
    {
        return await mediator.Send(command);
    }

    private static async Task<UpdateUser.Result> UpdateUser(IMediator mediator, UpdateUser.Command command)
    {
        return await mediator.Send(command);
    }

    private static async Task<DeleteUser.Result> DeleteUser(IMediator mediator, DeleteUser.Command command)
    {
        return await mediator.Send(command);
    }
}
