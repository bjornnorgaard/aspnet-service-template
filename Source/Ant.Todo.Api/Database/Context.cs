using Microsoft.EntityFrameworkCore;

namespace Ant.Todo.Api.Database
{
    public class Context : DbContext
    {
        public DbSet<Database.Models.Todo> Todos { get; set; }
        
        public Context(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        }
    }
}