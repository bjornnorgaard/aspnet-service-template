namespace Ant.Todo.Api.Controllers.Requests.Todos
{
    public class UpdateTodoRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}