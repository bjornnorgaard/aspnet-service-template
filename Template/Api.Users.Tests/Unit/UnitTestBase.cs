using AutoMapper;
using Xunit;

namespace Api.Todos.Tests.Unit;

[Collection("UnitTests")]
public abstract class UnitTestBase : IDisposable
{
    public IMapper Mapper { get; set; }

    /// <summary>
    /// This constructor is executed before each test.
    /// </summary>
    /// <param name="fixture"></param>
    public UnitTestBase(UnitTestFixture fixture)
    {
        Mapper = fixture.Mapper;
    }

    /// <summary>
    /// This Dispose() method is invoked after each test.
    /// </summary>
    public void Dispose()
    {
    }
}
