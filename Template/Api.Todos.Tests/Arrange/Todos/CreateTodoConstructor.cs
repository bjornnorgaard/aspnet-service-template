using System;
using Api.Todos.Database.Configurations;
using Api.Todos.Features.Todos;
using Bogus;

namespace Api.Todos.Tests.Arrange.Todos
{
    internal static class CreateTodoConstructor
    {
        public static CreateTodo.Command CreateValid(this CreateTodo.Command command)
        {
            var faker = new Faker();

            var createTodoCommand = new CreateTodo.Command
            {
                Title = faker.Commerce.ProductName(),
                Description = faker.Commerce.ProductDescription().Remove(TodoConstants.Description.MaxLength - 1),
                UserId = Guid.NewGuid().ToString()
            };

            return createTodoCommand;
        }
    }
}