using Api.Todos.Database;
using Xunit;

namespace Api.Todos.Tests.Integration;

public class IntegrationTestClassIsolation : IClassFixture<IntegrationTestMethodIsolation>
{
    public TodoContext Context { get; set; }
    public HttpClient Client { get; set; }

    public IntegrationTestClassIsolation(IntegrationTestMethodIsolation fixture)
    {
        Context = fixture.Context;
        Client = fixture.Client;
    }
}