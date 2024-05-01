using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Todos.Database.Configurations;

public class TodoConfiguration : IEntityTypeConfiguration<Models.Todo>
{
    public void Configure(EntityTypeBuilder<Models.Todo> todo)
    {
        todo.HasKey(t => t.Id);
        todo.Property(t => t.Title).HasMaxLength(TodoConstants.Title.MaxLength).IsRequired();
        todo.Property(t => t.Description).HasMaxLength(TodoConstants.Description.MaxLength).IsRequired(false);
        todo.Property(t => t.IsCompleted).HasDefaultValue(false);
    }
}