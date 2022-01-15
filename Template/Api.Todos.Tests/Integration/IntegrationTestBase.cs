using Api.Todos.Database;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

namespace Api.Todos.Tests.Integration;

[Collection("IntegrationTests")]
public abstract class IntegrationTestBase : IDisposable
{
    private readonly IDbContextTransaction _transaction;
    public TodoContext Context { get; set; }
    public HttpClient Client { get; set; }

    /// <summary>
    /// This constructor is executed before each test.
    /// </summary>
    /// <param name="fixture"></param>
    public IntegrationTestBase(IntegrationTestFixture fixture)
    {
        Client = fixture.Client;
        Context = fixture.Context;

        _transaction = Context.Database.BeginTransaction();
    }

    /// <summary>
    /// This Dispose() method is invoked after each test.
    /// </summary>
    public void Dispose()
    {
        // Clear database.
        _transaction.Rollback();
    }
}
