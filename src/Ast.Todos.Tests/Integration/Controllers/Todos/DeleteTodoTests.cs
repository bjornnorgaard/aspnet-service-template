using System.Net.Http.Json;
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
        var command = new Models.Todos.DeleteTodo.Command { TodoId = Guid.NewGuid().ToString() };

        // Act
        var response = await Client.PostAsJsonAsync(Routes.Todos.DeleteTodo, command);

        // Assert
        response.ShouldBeSuccess();
    }

    [Fact]
    public async Task ShouldFailToDeleteTodo_WhenTodoDoesNotExist()
    {
        // Arrange
        var todo = await Context.SeedTodoAsync();
        await Context.SaveChangesAsync();

        // Assert
        var deleteCommand = new Models.Todos.DeleteTodo.Command { TodoId = todo.Id.ToString() };
        var response = await Client.PostAsJsonAsync(Routes.Todos.DeleteTodo, deleteCommand);
        await Task.Delay(50); // Delete uses hangfire job, so we need to wait a bit.

        // Assert
        response.ShouldBeSuccess();
        var found = await Context.Todos.FindAsync(todo.Id);
        found?.Should().NotBeNull();
        found?.Title.Should().Be(todo.Title);
    }
}
