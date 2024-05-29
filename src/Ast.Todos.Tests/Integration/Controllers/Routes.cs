namespace Ast.Todos.Tests.Integration.Controllers;

internal static class Routes
{
    public static class Todos
    {
        public const string Base = "todos";
        public const string GetTodo = $"{Base}/get-todo";
        public const string GetTodos = $"{Base}/get-todos";
        public const string CreateTodo = $"{Base}/create-todo";
        public const string UpdateTodo = $"{Base}/update-todo";
        public const string DeleteTodo = $"{Base}/delete-todo";
    }
}
