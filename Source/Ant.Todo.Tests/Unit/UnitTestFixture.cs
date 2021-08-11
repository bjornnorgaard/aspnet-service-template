using System;
using Ant.Todo.Api;
using Ant.Todo.Api.Database;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ant.Todo.Tests.Unit
{
    public class UnitTestFixture : IDisposable
    {
        public TodoContext TodoContext { get; set; }
        public IMapper Mapper { get; set; }

        /// <summary>
        /// This constructor is run once before the suite starts.
        /// </summary>
        public UnitTestFixture()
        {
            var builder = new DbContextOptionsBuilder<TodoContext>();
            var cs = "Server=localhost;Database=Todo_UnitTest;User=sa;Password=Your_password123;";
            builder.UseSqlServer(cs);

            Mapper = new MapperConfiguration(c => c.AddMaps(typeof(Startup).Assembly)).CreateMapper();

            TodoContext = new TodoContext(builder.Options);
            TodoContext.Database.Migrate();
        }

        /// <summary>
        /// This Dispose() method is invoked after the entire suite has completed.
        /// </summary>
        public void Dispose()
        {
            // Clear, delete, rollback, whatever.
            TodoContext.Database.EnsureDeleted();
        }
    }
}