using System;
using System.Threading.Tasks;
using Ant.Todo.Api.Database;
using Bogus;

namespace Ant.Todo.Tests.Arrange
{
    public static class TodoConstructor
    {
        public static async Task<Api.Database.Models.Todo> SeedTodoAsync(this TodoContext todoContext,
            Api.Database.Models.Todo todo = null)
        {
            var faker = new Faker();

            if (todo == null)
            {
                todo = new Api.Database.Models.Todo
                {
                    Title = faker.Commerce.ProductName(),
                    Description = faker.Commerce.ProductDescription(),
                    UserId = Guid.NewGuid().ToString(),
                    IsCompleted = false,
                };
            }

            await todoContext.Todos.AddAsync(todo);
            return todo;
        }
    }
}