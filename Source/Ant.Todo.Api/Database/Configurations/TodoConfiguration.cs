using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ant.Todo.Api.Database.Configurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<Models.Todo>
    {
        public void Configure(EntityTypeBuilder<Models.Todo> todo)
        {
            todo.HasKey(t => t.Id);
            todo.Property(t => t.Title).HasMaxLength(25).IsRequired();
            todo.Property(t => t.Description).HasMaxLength(100).IsRequired(false);
            todo.Property(t => t.IsCompleted).HasDefaultValue(false);
        }
    }
}