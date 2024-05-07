using Ast.Todos.Database;
using Ast.Todos.Database.Configurations;
using FluentValidation;
using MediatR;

namespace Ast.Todos.Features.Todos;

public class CreateTodo
{
    public class Command : IRequest<Result>
    {
        public required string Title { get; init; }
        public required string Description { get; init; }
    }

    public class Result
    {
        public TodoDto CreatedTodo { get; init; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Title).NotEmpty()
                .MinimumLength(TodoConstants.Title.MinLength)
                .MaximumLength(TodoConstants.Title.MaxLength);

            RuleFor(c => c.Description)
                .MaximumLength(TodoConstants.Description.MaxLength);
        }
    }

    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly TodoContext _todoContext;

        public Handler(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public async Task<Result> Handle(Command request, CancellationToken ct)
        {
            var todo = request.MapToTodo();

            await _todoContext.Todos.AddAsync(todo, ct);
            await _todoContext.SaveChangesAsync(ct);

            var dto = todo.MapToDto();
            var result = new Result { CreatedTodo = dto };
            return result;
        }
    }
}