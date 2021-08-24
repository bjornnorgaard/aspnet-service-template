using System.Net.Http.Json;
using FluentAssertions;
using Svc.Todos.Api.Features.Todos;
using Xunit;

namespace Svc.Todos.Tests.Integration.Features.Todos
{
    public class GetTodosTests : IntegrationTestBase
    {
        public GetTodosTests(IntegrationTestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async void ShouldReturnEmptyList_WhenDatabaseIsEmpty()
        {
            // Arrange
            var command = new GetTodos.Command();

            // Act
            var response = await Client.PostAsJsonAsync("todos/get-todos", command);
            var content = await response.Content.ReadFromJsonAsync<GetTodos.Result>();

            // Assert
            content.Todos.Should().BeEmpty();
        }
    }
}