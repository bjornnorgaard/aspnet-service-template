using Ast.Todos.Database;
using AutoMapper;
using Xunit;

namespace Ast.Todos.Tests.Integration;

public class IntegrationTestClassIsolation : IClassFixture<IntegrationTestMethodIsolation>
{
    public TodoContext Context { get; set; }
    public HttpClient Client { get; set; }
    public IMapper Mapper { get; set; }

    public IntegrationTestClassIsolation(IntegrationTestMethodIsolation fixture)
    {
        Context = fixture.Context;
        Client = fixture.Client;
        Mapper = fixture.Mapper;
    }
}
