using System.Net;
using FluentAssertions;
using Xunit;

namespace Api.Todos.Tests.Integration.Middleware
{
    public class HealthCheckTests : IntegrationTestBase
    {
        public HealthCheckTests(IntegrationTestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async void ShouldReturnOkAndHealthy_WhenEndpointIsCalled()
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
}