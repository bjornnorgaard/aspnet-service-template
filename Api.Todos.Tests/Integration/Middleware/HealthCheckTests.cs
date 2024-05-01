using System.Net;
using FluentAssertions;
using Xunit;

namespace Api.Todos.Tests.Integration.Middleware;

public class HealthCheckTests : IntegrationTestCollectionIsolation
{
    public HealthCheckTests(IntegrationTestMethodIsolation fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ShouldReturnOkAndHealthy_WhenEndpointIsCalled()
    {
        // Arrange

        // Act
        var result = await Client.GetAsync("/hc");
        var content = await result.Content.ReadAsStringAsync();

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().Contain("Healthy");
    }
}