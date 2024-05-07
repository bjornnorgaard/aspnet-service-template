using System.Net.Http.Json;
using Ast.Todos.Controllers;
using Ast.Todos.Features.Todos;
using Ast.Todos.Tests.Arrange;
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
        var command = ArrangeCreateTodoCommand.CreateValid();

        // Act
        var response = await Client.PostAsJsonAsync(Routes.Todos.CreateTodo, command);
        var content = await response.Content.ReadFromJsonAsync<CreateTodo.Result>();

        // Assert
        content.CreatedTodo.Should().NotBeNull();
    }
}