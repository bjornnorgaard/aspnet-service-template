using Ast.Todos.Database.Models;
using Bogus;

namespace Ast.Todos.Tests.Arrange;

internal static class TodoExtensions
{
    public static Todo Valid(this Todo todo)
    {
        var faker = new Faker();

        todo.Id = faker.Random.Guid();
        todo.Title = faker.Commerce.ProductName();
        todo.Description = faker.Commerce.ProductAdjective();
        todo.IsCompleted = faker.Random.Bool();

        return todo;
    }
}