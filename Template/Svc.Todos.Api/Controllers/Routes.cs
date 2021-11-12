namespace Svc.Todos.Api.Controllers
{
    public static class Routes
    {
        private const string Api = "";

        public static class Todos
        {
            public const string Base = Api + "todos";
            public const string GetTodo = Base + "/get-todo";
            public const string GetTodos = Base + "/get-todos";
            public const string CreateTodo = Base + "/create-todo";
            public const string UpdateTodo = Base + "/update-todo";
            public const string DeleteTodo = Base + "/delete-todo";
        }
    }
}