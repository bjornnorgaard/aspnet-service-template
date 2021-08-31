using System;
using Bogus;
using Svc.Todos.Api.Database.Configurations;
using Svc.Todos.Api.Features.Todos;

namespace Svc.Todos.Tests.Arrange
{
    internal static class CreateTodoConstructor
    {
        public static CreateTodo.Command CreateValid(this CreateTodo.Command command)
        {
            var faker = new Faker();

            var createTodoCommand = new CreateTodo.Command
            {
                Title = faker.Commerce.ProductName(),
                Description = faker.Commerce.ProductDescription().Remove(TodoConstants.Description.MaxLength),
                UserId = Guid.NewGuid().ToString()
            };

            return createTodoCommand;
        }
    }
}