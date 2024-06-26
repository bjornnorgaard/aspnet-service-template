using System.Net;
using FluentAssertions;
using Xunit;

namespace Ast.Todos.Tests.Integration.Middleware;

public class SwaggerTests : IntegrationTestCollectionIsolation
{
    public SwaggerTests(IntegrationTestMethodIsolation fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ShouldReturnOk_WhenSwaggerDocsWorks()
    {
        // Arrange

        // Act
        var result = await Client.GetAsync("swagger/v1/swagger.json");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}