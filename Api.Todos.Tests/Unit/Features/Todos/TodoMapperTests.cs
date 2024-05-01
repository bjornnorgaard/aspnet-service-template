using AutoMapper;
using Xunit;

namespace Api.Todos.Tests.Unit.Features.Todos;

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