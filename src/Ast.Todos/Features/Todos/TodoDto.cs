namespace Ast.Todos.Features.Todos;

public class TodoDto
{
    public Guid Id { get; init; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}