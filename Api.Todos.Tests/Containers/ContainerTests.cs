using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Api.Todos.Tests.Containers;

public class ContainerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ContainerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task TestSomething()
    {
        var res = await _client.GetStringAsync("/hc");
        res.Should().Be("Healthy!");
    }
}