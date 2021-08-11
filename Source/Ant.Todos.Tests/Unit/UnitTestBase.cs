using System;
using Ant.Todos.Api.Database;
using AutoMapper;
using Xunit;

namespace Ant.Todos.Tests.Unit
{
    [Collection("UnitTests")]
    public abstract class UnitTestBase : IDisposable
    {
        public TodoContext TodoContext { get; set; }
        public IMapper Mapper { get; set; }

        /// <summary>
        /// This constructor is executed before each test.
        /// </summary>
        /// <param name="fixture"></param>
        public UnitTestBase(UnitTestFixture fixture)
        {
            TodoContext = fixture.TodoContext;
            Mapper = fixture.Mapper;

            TodoContext.Database.BeginTransaction();
        }

        /// <summary>
        /// This Dispose() method is invoked after each test.
        /// </summary>
        public void Dispose()
        {
            TodoContext.Database.RollbackTransaction();
        }
    }
}