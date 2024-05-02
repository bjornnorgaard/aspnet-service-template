using Ast.Todos.Features.Todos;
using FluentAssertions;
using Xunit;

namespace Ast.Todos.Tests.TestIntegration.Features.Todos;

public class GetTodosTests : IntegrationTestCollectionIsolation
{
    private readonly GetTodos.Handler _uut;

    public GetTodosTests(IntegrationTestMethodIsolation fixture) : base(fixture)
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