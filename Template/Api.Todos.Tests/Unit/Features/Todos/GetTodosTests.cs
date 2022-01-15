using Api.Todos.Features.Todos;
using FluentAssertions;
using Xunit;

namespace Api.Todos.Tests.Unit.Features.Todos
{
    public class GetTodosTests : UnitTestBase
    {
        private readonly GetTodos.Handler _uut;

        public GetTodosTests(UnitTestFixture fixture) : base(fixture)
        {
            _uut = new GetTodos.Handler(TodoContext, Mapper);
        }

        [Fact]
        public async void ShouldReturnEmptyList_WhenDatabaseIsEmpty()
        {
            // Arrange
            var command = new GetTodos.Command { };

            // Act
            var result = await _uut.Handle(command, CancellationToken.None);

            // Assert
            result.Todos.Should().BeEmpty();
        }
    }
}