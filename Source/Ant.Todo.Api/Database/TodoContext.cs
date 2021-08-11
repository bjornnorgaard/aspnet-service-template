using Microsoft.EntityFrameworkCore;

namespace Ant.Todo.Api.Database
{
    public class TodoContext : DbContext
    {
        public DbSet<Database.Models.Todo> Todos { get; set; }

        public TodoContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoContext).Assembly);
        }
    }
}