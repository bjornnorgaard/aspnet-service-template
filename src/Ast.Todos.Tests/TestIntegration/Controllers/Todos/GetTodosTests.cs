using System.Net.Http.Json;
using Ast.Todos.Controllers;
using Ast.Todos.Features.Todos;
using Ast.Todos.Tests.Arrange;
using FluentAssertions;
using Xunit;

namespace Ast.Todos.Tests.TestIntegration.Controllers.Todos;

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