using System.Net.Http.Json;
using Ast.Todos.Tests.Arrange;
using Ast.Todos.Tests.Assertions;
using FluentAssertions;
using Xunit;

namespace Ast.Todos.Tests.Integration.Controllers.Todos;

public class GetTodosTests : IntegrationTestCollectionIsolation
{
    public GetTodosTests(IntegrationTestMethodIsolation fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ShouldReturnPagedListOfTodos()
    {
        // Arrange
        for (var i = 0; i < 10; i++)
        {
            await Context.SeedTodoAsync();
        }

        await Context.SaveChangesAsync();

        var command = new Models.Todos.GetTodos.Command { PageSize = 7 };

        // Act
        var response = await Client.PostAsJsonAsync(Routes.Todos.GetTodos, command);
        var content = await response.Content.ReadFromJsonAsync<Models.Todos.GetTodos.Result>();

        // Assert
        response.ShouldBeSuccess();
        content.Todos.Should().HaveCount(7);

        foreach (var todo in content.Todos)
        {
            todo.Id.MustBeValidGuid();
            todo.Title.Should().NotBeNullOrWhiteSpace();
            todo.Description.Should().NotBeNullOrWhiteSpace();
            todo.IsCompleted.Should().BeFalse();
        }
    }
}
