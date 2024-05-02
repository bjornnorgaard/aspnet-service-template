using System.Net.Http.Json;
using Ast.Todos.Controllers;
using Ast.Todos.Database.Configurations;
using Ast.Todos.Features.Todos;
using Ast.Todos.Tests.Arrange;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Ast.Todos.Tests.TestIntegration.Controllers.Todos;

public class UpdateTodoTests : IntegrationTestCollectionIsolation
{
    public UpdateTodoTests(IntegrationTestMethodIsolation fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ShouldFailToUpdateTodo_WhenTitleIsTooLong()
    {
        // Arrange
        var originalTodo = await Context.SeedTodoAsync();
        var faker = new Faker();

        var tooLongTitle = faker.Random.String(TodoConstants.Title.MaxLength + 1);

        var updateCommand = new UpdateTodo.Command
        {
            TodoId = originalTodo.Id,
            Title = tooLongTitle,
            Description = originalTodo.Description,
            IsCompleted = originalTodo.IsCompleted,
        };

        // Act
        var response = await Client.PostAsJsonAsync(Routes.Todos.UpdateTodo, updateCommand);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
    }
}