using System.Net.Http.Json;
using Ast.Todos.Tests.Assertions;
using FluentAssertions;
using Xunit;

namespace Ast.Todos.Tests.Integration.Controllers.Todos;

public class CreateTodoTests : IntegrationTestCollectionIsolation
{
    public CreateTodoTests(IntegrationTestMethodIsolation fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ShouldCreateTodo_WhenTodoIsValid()
    {
        // Arrange
        var command = new Models.Todos.CreateTodo.Command();

        // Act
        var response = await Client.PostAsJsonAsync(Routes.Todos.CreateTodo, command);
        var content = await response.Content.ReadFromJsonAsync<Models.Todos.CreateTodo.Result>();

        // Assert
        response.ShouldBeSuccess();
        content.CreatedTodo.Should().BeEquivalentTo(command);
        content.CreatedTodo.Id.MustBeValidGuid();
        content.CreatedTodo.IsCompleted.Should().BeFalse();
    }
}
