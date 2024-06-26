using Ast.Todos.Database.Models;
using Ast.Todos.Features.Todos;
using Ast.Todos.Tests.Arrange;
using FluentAssertions;
using Xunit;

namespace Ast.Todos.Tests.Unit;

public class TodoMapperTests
{
    [Fact]
    public void MapDto()
    {
        // Arrange
        var todo = new Todo().Valid();

        // Act
        var dto = todo.MapToDto();

        // Assert
        dto.Should().BeEquivalentTo(todo);
    }
}
