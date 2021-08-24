using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Svc.Todos.Tests.Integration.Middleware
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
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            content.Should().Contain("Healthy");
        }
    }
}