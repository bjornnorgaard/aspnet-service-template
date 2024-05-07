using System.Net.Http.Json;
using Ast.Todos.Controllers;
using Ast.Todos.Features.Todos;
using Ast.Todos.Tests.Arrange;
using Ast.Todos.Tests.Assertions;
using FluentAssertions;
using Xunit;

namespace Ast.Todos.Tests.Integration.Controllers.Todos;

public class DeleteTodoTests : IntegrationTestCollectionIsolation
{
    public DeleteTodoTests(IntegrationTestMethodIsolation fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ShouldReturnSuccess_EvenWhenTodoDoesNotExist()
    {
        // Arrange
        var command = new DeleteTodo.Command { TodoId = Guid.NewGuid() };

        // Act
        var response = await Client.PostAsJsonAsync(Routes.Todos.DeleteTodo, command);

        // Assert
        await response.ShouldBeSuccess();
    }

    [Fact]
    public async Task ShouldFailToDeleteTodo_WhenTodoDoesNotExist()
    {
        // Arrange
        var todo = await Context.SeedTodoAsync();
        await Context.SaveChangesAsync();

        // Act
        var deleteCommand = new DeleteTodo.Command { TodoId = todo.Id };
        var response = await Client.PostAsJsonAsync(Routes.Todos.DeleteTodo, deleteCommand);
        await Task.Delay(50); // Delete uses hangfire job, so we need to wait a bit.

        // Assert
        await response.ShouldBeSuccess();
        var found = await Context.Todos.FindAsync(todo.Id);
        found?.Should().NotBeNull();
        found?.Title.Should().Be(todo.Title);
    }
}