using System;
using System.Threading.Tasks;
using Ant.Todos.Api.Database;
using Ant.Todos.Api.Database.Models;
using Bogus;

namespace Ant.Todos.Tests.Arrange
{
    public static class TodoConstructor
    {
        public static async Task<Todo> SeedTodoAsync(this TodoContext todoContext, Todo todo = null)
        {
            var faker = new Faker();

            if (todo == null)
            {
                todo = new Todo
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