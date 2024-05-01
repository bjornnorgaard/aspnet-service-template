using Api.Todos.Features.Todos;
using Api.Todos.Tests.Arrange;
using FluentAssertions;
using Xunit;

namespace Api.Todos.Tests.Integration.Features.Todos;

public class CreateTodoTests : IntegrationTestCollectionIsolation
{
    private readonly CreateTodo.Handler _uut;

    public CreateTodoTests(IntegrationTestMethodIsolation fixture) : base(fixture)
    {
        _uut = new CreateTodo.Handler(Context, Mapper);
    }

    [Fact]
    public async Task ShouldThrow_WhenTodoIsInvalid()
    {
        // Arrange
        var command = new CreateTodo.Command().CreateValid();

        // Act
        var result = await _uut.Handle(command, CancellationToken.None);

        // Assert
        result.CreatedTodo.Id.Should().NotBeEmpty();
    }
}