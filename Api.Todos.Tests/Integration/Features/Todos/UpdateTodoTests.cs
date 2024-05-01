using System.Net.Http.Json;
using Api.Todos.Controllers;
using Api.Todos.Database.Configurations;
using Api.Todos.Features.Todos;
using Api.Todos.Tests.Arrange.Todos;
using Api.Todos.Tests.Extensions;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Api.Todos.Tests.Integration.Features.Todos;

public class UpdateTodoTests : IntegrationTestBase
{
    public UpdateTodoTests(IntegrationTestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ShouldFailToUpdateTodo_WhenTitleIsTooLong()
    {
        // Arrange
        var originalTodo = await Context.SeedTodoAsync();
        var faker = new Faker();

        var tooLongTitle = faker.Random.String(TodoConstants.Title.MaxLength + 1);
        
        var updateCommand = new UpdateTodo.Command
        {
            TodoId = originalTodo.Id,
            Title = tooLongTitle,
            Description = originalTodo.Description,
            IsCompleted = originalTodo.IsCompleted,
        };

        // Act
        var response = await Client.PostAsJsonAsync(Routes.Todos.UpdateTodo, updateCommand);
        
        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
    }

    [Fact]
    public async Task ShouldUpdateTodo_WhenTodoIsValid()
    {
        // Arrange
        var originalTodo = await Context.SeedTodoAsync();
 
        var updateCommand = new UpdateTodo.Command
        {
            TodoId = originalTodo.Id,
            Title = "Updated title",
            Description = originalTodo.Description,
            IsCompleted = originalTodo.IsCompleted,
        };

        // Act
        var response = await Client.PostAsJsonAsync(Routes.Todos.UpdateTodo, updateCommand);
        var content = await response.Content.ReadFromJsonAsync<UpdateTodo.Result>();
        var updatedTodo = content.UpdatedTodo;

        // Assert
        await response.ShouldBeSuccess();
        updatedTodo.Id.Should().Be(originalTodo.Id);
        updatedTodo.Title.Should().Be(updateCommand.Title);
        updatedTodo.Description.Should().Be(updateCommand.Description);
        updatedTodo.IsCompleted.Should().Be(updateCommand.IsCompleted);
    }
}