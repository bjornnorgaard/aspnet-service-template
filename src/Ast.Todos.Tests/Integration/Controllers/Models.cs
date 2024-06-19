using Bogus;

namespace Ast.Todos.Tests.Integration.Controllers;

internal class Models
{
    public class BadRequestResponse
    {
        public int Code { get; init; }
        public string Message { get; init; }
    }

    public class Todos
    {
        public class CreateTodo
        {
            public class Command
            {
                public string Title { get; set; }
                public string Description { get; set; }

                public Command()
                {
                    var faker = new Faker();
                    Title = faker.Commerce.ProductName();
                    Description = faker.Commerce.ProductAdjective();
                }
            }

            public class Result
            {
                public TodoDto CreatedTodo { get; set; }
            }
        }

        public class DeleteTodo
        {
            public class Command
            {
                public required string TodoId { get; init; }
            }
        }

        public class GetTodos
        {
            public class Command
            {
                public int PageNumber { get; init; } = 0;
                public int PageSize { get; init; } = 10;
                public string SortProperty { get; init; } = nameof(TodoDto.Id);
                public string SortOrder { get; init; } = "desc";
            }

            public class Result
            {
                public IEnumerable<TodoDto> Todos { get; init; }
            }
        }

        public class GetTodo
        {
            public class Command
            {
                public required string TodoId { get; init; }
            }

            public class Result
            {
                public TodoDto Todo { get; set; }
            }
        }

        public class UpdateTodo
        {
            public class Command
            {
                public required string Title { get; init; }
                public required string Description { get; init; }
                public required bool IsCompleted { get; init; }
                public required Guid TodoId { get; init; }
            }

            public class Result
            {
                public TodoDto UpdatedTodo { get; set; }
            }
        }

        public class TodoDto
        {
            public string Id { get; init; }
            public string Title { get; set; }
            public string Description { get; set; }
            public bool IsCompleted { get; set; }
        }
    }
}