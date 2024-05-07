using Ast.Todos.Database.Models;

namespace Ast.Todos.Features.Todos;

public static class TodoMapper
{
    public static Todo MapToTodo(this CreateTodo.Command command)
    {
        return new Todo
        {
            Title = command.Title,
            Description = command.Description
        };
    }

    public static Todo MapToTodo(this UpdateTodo.Command command)
    {
        return new Todo
        {
            Id = command.TodoId,
            Title = command.Title,
            Description = command.Description,
            IsCompleted = command.IsCompleted
        };
    }

    public static TodoDto MapToDto(this Todo todo)
    {
        return new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted
        };
    }

    public static IEnumerable<TodoDto> TodoDtos(this IEnumerable<Todo> todos)
    {
        return todos.Select(t => t.MapToDto());
    }
}