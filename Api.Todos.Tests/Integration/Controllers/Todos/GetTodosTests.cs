using System.Net.Http.Json;
using Api.Todos.Controllers;
using Api.Todos.Features.Todos;
using Api.Todos.Tests.Arrange;
using FluentAssertions;
using Xunit;

namespace Api.Todos.Tests.Integration.Controllers.Todos;

public class GetTodosTests : IntegrationTestCollectionIsolation
{
    public GetTodosTests(IntegrationTestMethodIsolation fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ShouldReturnEmptyList()
    {
        // Arrange
        var command = new GetTodos.Command();

        // Act
        var response = await Client.PostAsJsonAsync(Routes.Todos.GetTodos, command);
        var content = await response.Content.ReadFromJsonAsync<GetTodos.Result>();

        // Assert
        content.Todos.Should().NotBeNull();
    }

    [Fact]
    public async Task ShouldReturnListOfTodos()
    {
        // Arrange
        for (var i = 0; i < 10; i++)
        {
            await Context.SeedTodoAsync();
        }

        await Context.SaveChangesAsync();

        var command = new GetTodos.Command { PageSize = 7 };

        // Act
        var response = await Client.PostAsJsonAsync(Routes.Todos.GetTodos, command);
        var content = await response.Content.ReadFromJsonAsync<GetTodos.Result>();

        // Assert
        content.Todos.Should().HaveCount(7);
    }
}