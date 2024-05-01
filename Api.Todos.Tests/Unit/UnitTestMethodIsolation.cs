using Api.Todos.Database;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;
using Xunit;

namespace Api.Todos.Tests.Unit;

[CollectionDefinition(nameof(UnitTestCollection))]
public class UnitTestCollection : ICollectionFixture<UnitTestMethodIsolation>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

public class UnitTestMethodIsolation : IAsyncDisposable
{
    public TodoContext Context { get; set; }
    public IMapper Mapper { get; set; }

    private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder().Build();

    public UnitTestMethodIsolation()
    {
        _sqlContainer.StartAsync().Wait();

        var cs = _sqlContainer.GetConnectionString();
        Context = new TodoContext(new DbContextOptionsBuilder<TodoContext>().UseSqlServer(cs).Options);
        Context.Database.Migrate();

        Mapper = new MapperConfiguration(c => c.AddMaps(typeof(AssemblyAnchor).Assembly)).CreateMapper();
    }

    public async ValueTask DisposeAsync()
    {
        await _sqlContainer.DisposeAsync();
        await Context.DisposeAsync();
    }
}