using System.Net.Http.Json;
using Api.Todos.Controllers;
using Api.Todos.Features.Todos;
using FluentAssertions;
using Xunit;

namespace Api.Todos.Tests.Integration.Features.Todos;

public class GetTodosTests : IntegrationTestBase
{
    public GetTodosTests(IntegrationTestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async void ShouldReturnEmptyList_WhenDatabaseIsEmpty()
    {
        // Arrange
        var command = new GetTodos.Command();

        // Act
        var response = await Client.PostAsJsonAsync(Routes.Todos.GetTodos, command);
        var content = await response.Content.ReadFromJsonAsync<GetTodos.Result>();

        // Assert
        content.Todos.Should().BeEmpty();
    }
}