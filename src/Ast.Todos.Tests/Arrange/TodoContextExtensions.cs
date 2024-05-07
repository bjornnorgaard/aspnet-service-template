using Ast.Todos.Database;
using Ast.Todos.Database.Models;
using Bogus;

namespace Ast.Todos.Tests.Arrange;

internal static class TodoContextExtensions
{
    public static async Task<Todo> SeedTodoAsync(this TodoContext context, Todo todo = null)
    {
        var faker = new Faker();

        if (todo == null)
        {
            todo = new Todo
            {
                Title = faker.Commerce.ProductName(),
                Description = faker.Commerce.ProductAdjective(),
                IsCompleted = false,
            };
        }

        await context.Todos.AddAsync(todo);

        return todo;
    }
}