using System.Threading.Tasks;
using Ant.Todo.Api.Database;

namespace Ant.Todo.Tests.Arrange
{
    public static class TodoConstructor
    {
        public static async Task<Api.Database.Models.Todo> SeedTodoAsync(this TodoContext todoContext,
            Api.Database.Models.Todo todo = null)
        {
            if (todo == null)
            {
                todo = new Api.Database.Models.Todo
                {
                    Title = "Title",
                    Description = "Description",
                    UserId = "123-456"
                };
            }

            await todoContext.Todos.AddAsync(todo);
            return todo;
        }
    }
}