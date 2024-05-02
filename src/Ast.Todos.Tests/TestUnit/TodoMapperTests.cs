using AutoMapper;
using Xunit;

namespace Ast.Todos.Tests.TestUnit;

public class TodoMapperTests
{
    private readonly IMapper _uut;

    public TodoMapperTests()
    {
        _uut = new MapperConfiguration(c => c.AddMaps(typeof(AssemblyAnchor).Assembly)).CreateMapper();
    }

    [Fact]
    public void AssertValidConfiguration()
    {
        _uut.ConfigurationProvider.AssertConfigurationIsValid();
    }
}
