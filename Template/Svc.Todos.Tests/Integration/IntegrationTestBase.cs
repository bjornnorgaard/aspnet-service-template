using System;
using System.Net.Http;
using Svc.Todos.Api.Database;
using Xunit;

namespace Svc.Todos.Tests.Integration
{
    [Collection("IntegrationTests")]
    public abstract class IntegrationTestBase : IDisposable
    {
        public TodoContext Context { get; set; }
        public HttpClient Client { get; set; }

        /// <summary>
        /// This constructor is executed before each test.
        /// </summary>
        /// <param name="fixture"></param>
        public IntegrationTestBase(IntegrationTestFixture fixture)
        {
            Client = fixture.Client;
            Context = fixture.TodoContext;
        }

        /// <summary>
        /// This Dispose() method is invoked after each test.
        /// </summary>
        public void Dispose()
        {
            // Clear database.
        }
    }
}