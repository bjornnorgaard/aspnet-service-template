using Api.Todos.Database;
using AutoMapper;
using Xunit;

namespace Api.Todos.Tests.Unit;

[Collection(nameof(UnitTestCollection))]
public class UnitTestCollectionIsolation
{
    public IMapper Mapper { get; set; }
    public TodoContext Context { get; set; }

    protected UnitTestCollectionIsolation(UnitTestMethodIsolation fixture)
    {
        Context = fixture.Context;
        Mapper = fixture.Mapper;
    }
}