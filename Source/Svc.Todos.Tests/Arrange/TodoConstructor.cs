using System;
using System.Threading.Tasks;
using Bogus;
using Svc.Todos.Api.Database;
using Svc.Todos.Api.Database.Models;

namespace Svc.Todos.Tests.Arrange
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