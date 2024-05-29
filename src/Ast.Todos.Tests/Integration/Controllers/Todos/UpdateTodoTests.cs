using System.Net.Http.Json;
using Ast.Todos.Database.Configurations;
using Ast.Todos.Tests.Arrange;
using Ast.Todos.Tests.Assertions;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Ast.Todos.Tests.Integration.Controllers.Todos;

public class UpdateTodoTests : IntegrationTestCollectionIsolation
{
    public UpdateTodoTests(IntegrationTestMethodIsolation fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ShouldFailToUpdateTodo_WhenTitleIsTooLong()
    {
        // Arrange
        var originalTodo = await Context.SeedTodoAsync();
        await Context.SaveChangesAsync();

        var faker = new Faker();

        var tooLongTitle = faker.Random.String(TodoConstants.Title.MaxLength + 1);

        var updateCommand = new Models.Todos.UpdateTodo.Command
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
    public async Task ShouldUpdateTodo()
    {
        // Arrange
        var originalTodo = await Context.SeedTodoAsync();
        await Context.SaveChangesAsync();

        var updateCommand = new Models.Todos.UpdateTodo.Command
        {
            TodoId = originalTodo.Id,
            Title = "updated title",
            Description = originalTodo.Description,
            IsCompleted = true,
        };

        // Act
        var response = await Client.PostAsJsonAsync(Routes.Todos.UpdateTodo, updateCommand);
        var content = await response.Content.ReadFromJsonAsync<Models.Todos.UpdateTodo.Result>();

        // Assert
        response.ShouldBeSuccess();
        content.UpdatedTodo.Id.MustBeValidGuid();
        content.UpdatedTodo.Title.Should().Be("updated title");
        content.UpdatedTodo.Description.Should().NotBeNullOrWhiteSpace();
        content.UpdatedTodo.IsCompleted.Should().BeTrue();
    }
}
