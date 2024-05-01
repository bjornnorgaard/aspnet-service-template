using Api.Todos.Features.Todos;
using FluentAssertions;
using Xunit;

namespace Api.Todos.Tests.Unit.Features.Todos;

public class GetTodosTests : UnitTestCollectionIsolation
{
    private readonly GetTodos.Handler _uut;
    
    public GetTodosTests(UnitTestMethodIsolation fixture) : base(fixture)
    {
        _uut = new GetTodos.Handler(Context, Mapper);
    }

    [Fact]
    public async Task ShouldReturnEmptyList_WhenDatabaseIsEmpty()
    {
        // Arrange
        var command = new GetTodos.Command();

        // Act
        var result = await _uut.Handle(command, CancellationToken.None);

        // Assert
        result.Todos.Should().NotBeNull();
    }
}