using Ast.Users.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ast.Users.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> todo)
    {
        todo.HasKey(t => t.Id);
        todo.Property(t => t.Name).HasMaxLength(UserConstants.Name.MaxLength).IsRequired();
    }
}
