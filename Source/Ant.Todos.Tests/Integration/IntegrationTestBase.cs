using System;
using System.Net.Http;
using Ant.Todos.Api.Database;
using Xunit;

namespace Ant.Todos.Tests.Integration
{
    [Collection("IntegrationTests")]
    public abstract class IntegrationTestBase : IDisposable
    {
        public TodoContext TodoContext { get; set; }
        public HttpClient Client { get; set; }

        /// <summary>
        /// This constructor is executed before each test.
        /// </summary>
        /// <param name="fixture"></param>
        public IntegrationTestBase(IntegrationTestFixture fixture)
        {
            Client = fixture.Client;
            TodoContext = fixture.TodoContext;
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