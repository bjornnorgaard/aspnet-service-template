using System;
using System.Threading.Tasks;
using Bogus;
using Svc.Todos.Api.Database;
using Svc.Todos.Api.Database.Configurations;
using Svc.Todos.Api.Database.Models;

namespace Svc.Todos.Tests.Arrange
{
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
                    Description = faker.Commerce.ProductDescription().Remove(TodoConstants.Description.MaxLength),
                    UserId = Guid.NewGuid().ToString(),
                    IsCompleted = false,
                };
            }

            await context.Todos.AddAsync(todo);

            return todo;
        }
    }
}