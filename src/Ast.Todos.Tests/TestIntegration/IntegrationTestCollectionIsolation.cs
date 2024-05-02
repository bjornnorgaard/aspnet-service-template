using Ast.Todos.Database;
using AutoMapper;
using Xunit;

namespace Ast.Todos.Tests.TestIntegration;

[Collection(nameof(IntegrationTestCollection))]
public class IntegrationTestCollectionIsolation
{
    public TodoContext Context { get; set; }
    public HttpClient Client { get; set; }
    public IMapper Mapper { get; set; }

    public IntegrationTestCollectionIsolation(IntegrationTestMethodIsolation fixture)
    {
        Context = fixture.Context;
        Client = fixture.Client;
        Mapper = fixture.Mapper;
    }
}