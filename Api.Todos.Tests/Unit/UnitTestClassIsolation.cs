using Api.Todos.Database;
using AutoMapper;
using Xunit;

namespace Api.Todos.Tests.Unit;

public class UnitTestClassIsolation : IClassFixture<UnitTestMethodIsolation>
{
    public IMapper Mapper { get; set; }
    public TodoContext Context { get; set; }

    public UnitTestClassIsolation(UnitTestMethodIsolation fixture)
    {
        Context = fixture.Context;
        Mapper = fixture.Mapper;
    }
}