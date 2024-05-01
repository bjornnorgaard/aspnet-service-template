using System.Net.Http.Json;
using Api.Todos.Controllers;
using Api.Todos.Features.Todos;
using Api.Todos.Tests.Arrange.Todos;
using FluentAssertions;
using Xunit;

namespace Api.Todos.Tests.Integration.Features.Todos;

public class CreateTodoTests : IntegrationTestCollectionIsolation
{
    public CreateTodoTests(IntegrationTestMethodIsolation fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ShouldCreateTodo_WhenTodoIsValid()
    {
        // Arrange
        var command = new CreateTodo.Command().CreateValid();

        // Act
        var response = await Client.PostAsJsonAsync(Routes.Todos.CreateTodo, command);
        var content = await response.Content.ReadFromJsonAsync<CreateTodo.Result>();

        // Assert
       content.CreatedTodo.Should().NotBeNull();
    }
}