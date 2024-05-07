using Ast.Todos.Database;
using Xunit;

namespace Ast.Todos.Tests.Integration;

[Collection(nameof(IntegrationTestCollection))]
public class IntegrationTestCollectionIsolation
{
    public TodoContext Context { get; set; }
    public HttpClient Client { get; set; }

    public IntegrationTestCollectionIsolation(IntegrationTestMethodIsolation fixture)
    {
        Context = fixture.Context;
        Client = fixture.Client;
    }
}