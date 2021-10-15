using System.Threading;
using FluentAssertions;
using Svc.Todos.Api.Features.Todos;
using Svc.Todos.Tests.Arrange;
using Xunit;

namespace Svc.Todos.Tests.Unit.Features.Todos
{
    public class CreateTodoTests : UnitTestBase
    {
        private readonly CreateTodo.Handler _uut;

        public CreateTodoTests(UnitTestFixture fixture) : base(fixture)
        {
            _uut = new CreateTodo.Handler(TodoContext, Mapper);
        }

        [Fact]
        public async void ShouldThrow_WhenTodoIsInvalid()
        {
            // Arrange
            var command = new CreateTodo.Command().CreateValid();

            // Act
            var result = await _uut.Handle(command, CancellationToken.None);

            // Assert
            result.CreatedTodo.Id.Should().NotBeEmpty();
        }
    }
}
