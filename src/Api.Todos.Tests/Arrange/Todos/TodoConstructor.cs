using Api.Todos.Database;
using Api.Todos.Database.Models;
using Bogus;

namespace Api.Todos.Tests.Arrange.Todos;

internal static class TodoConstructor
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
                UserId = Guid.NewGuid().ToString(),
                IsCompleted = false,
            };
        }

        await context.Todos.AddAsync(todo);

        return todo;
    }
}
