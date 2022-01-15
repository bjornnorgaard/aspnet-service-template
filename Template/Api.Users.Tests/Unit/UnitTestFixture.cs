using AutoMapper;

namespace Api.Todos.Tests.Unit;

public class UnitTestFixture : IDisposable
{
    public IMapper Mapper { get; set; }

    /// <summary>
    /// This constructor is run once before the suite starts.
    /// </summary>
    public UnitTestFixture()
    {
        Mapper = new MapperConfiguration(c => c.AddMaps(typeof(AssemblyAnchor).Assembly)).CreateMapper();
    }

    /// <summary>
    /// This Dispose() method is invoked after the entire suite has completed.
    /// </summary>
    public void Dispose()
    {
        // Clear, delete, rollback, whatever.
    }
}
