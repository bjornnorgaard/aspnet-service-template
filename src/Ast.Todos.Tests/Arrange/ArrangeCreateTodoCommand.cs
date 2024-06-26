﻿using Ast.Todos.Features.Todos;
using Bogus;

namespace Ast.Todos.Tests.Arrange;

internal static class ArrangeCreateTodoCommand
{
    public static CreateTodo.Command CreateValid()
    {
        var faker = new Faker();

        var createTodoCommand = new CreateTodo.Command
        {
            Title = faker.Commerce.ProductName(),
            Description = faker.Commerce.ProductAdjective(),
        };

        return createTodoCommand;
    }
}