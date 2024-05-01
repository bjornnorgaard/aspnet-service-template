using Api.Todos.Features.Todos;
using Api.Todos.Tests.Arrange.Todos;
using FluentAssertions;
using Xunit;

namespace Api.Todos.Tests.Unit.Features.Todos;

public class CreateTodoTests : UnitTestCollectionIsolation
{
    private readonly CreateTodo.Handler _uut;

    public CreateTodoTests(UnitTestMethodIsolation fixture) : base(fixture)
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