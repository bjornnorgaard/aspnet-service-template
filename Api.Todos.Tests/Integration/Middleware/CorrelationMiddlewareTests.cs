using FluentAssertions;
using Xunit;

namespace Api.Todos.Tests.Integration.Middleware;

public class CorrelationMiddlewareTests : IntegrationTestBase
{
    private const string HeaderName = "x-correlation-id";

    public CorrelationMiddlewareTests(IntegrationTestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ShouldReturnHeaderWithCorrelationId_WhenNoCorrelationIdIsProvided()
    {
        // Arrange
        Client.DefaultRequestHeaders.Remove(HeaderName);

        // Act
        var result = await Client.GetAsync("/hc");
        var found = result.Headers.TryGetValues(HeaderName, out var id);

        // Assert
        id.Should().NotBeNullOrEmpty();
        found.Should().BeTrue();
    }

    [Fact]
    public async Task ShouldReturnHeaderWithTheSameCorrelationId_WhenCorrelationIdIsProvided()
    {
        // Arrange
        const string expectation = "im the correlation id";
        Client.DefaultRequestHeaders.Add(HeaderName, expectation);

        // Act
        var result = await Client.GetAsync("/hc");
        result.Headers.TryGetValues(HeaderName, out var id);

        // Assert
        id.Should().BeEquivalentTo(expectation);
    }
}