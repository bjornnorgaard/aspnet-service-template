using Ast.Users.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Ast.Users.Database;

public class UserContext : DbContext
{
    public DbSet<User> Users { get; init; }

    public UserContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserContext).Assembly);
    }
}
