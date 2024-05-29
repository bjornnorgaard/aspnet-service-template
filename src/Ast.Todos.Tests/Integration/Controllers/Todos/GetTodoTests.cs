using System.Net;
using System.Net.Http.Json;
using Ast.Platform.Exceptions;
using FluentAssertions;
using Humanizer;
using Xunit;

namespace Ast.Todos.Tests.Integration.Controllers.Todos;

public class GetTodoTests : IntegrationTestCollectionIsolation
{
    public GetTodoTests(IntegrationTestMethodIsolation fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ShouldReturnBadRequest_WhenTodoDoesNotExist()
    {
        // Arrange
        var command = new Models.Todos.GetTodo.Command { TodoId = Guid.NewGuid().ToString() };

        // Act
        var response = await Client.PostAsJsonAsync(Routes.Todos.GetTodo, command);
        var content = await response.Content.ReadFromJsonAsync<Models.BadRequestResponse>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        content.Code.Should().Be((int)PlatformError.TodoNotFound);
        content.Message.Should().Be(PlatformError.TodoNotFound.Humanize(LetterCasing.Sentence));
    }
}
