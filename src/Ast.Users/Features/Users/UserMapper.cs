using Ast.Users.Database.Models;

namespace Ast.Users.Features.Users;

internal static class UserMapper
{
    internal static User MapToUser(this CreateUser.Command command)
    {
        return new User
        {
            Name = command.Name
        };
    }

    internal static User MapToUser(this UpdateUser.Command command)
    {
        return new User
        {
            Id = command.Id,
            Name = command.Name,
        };
    }

    internal static UserDto MapToDto(this User todo)
    {
        return new UserDto
        {
            Id = todo.Id,
            Name = todo.Name,
        };
    }

    internal static IEnumerable<UserDto> UserDtos(this IEnumerable<User> todos)
    {
        return todos.Select(t => t.MapToDto());
    }
}
