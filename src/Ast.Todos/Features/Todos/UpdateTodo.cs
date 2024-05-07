using Ast.Platform.Exceptions;
using Ast.Todos.Database;
using Ast.Todos.Database.Configurations;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ast.Todos.Features.Todos;

public class UpdateTodo
{
    public class Command : IRequest<Result>
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

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.TodoId).NotEmpty();

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
            var todo = await _todoContext.Todos.AsTracking()
                .Where(t => t.Id == request.TodoId)
                .FirstOrDefaultAsync(ct);

            if (todo == null) throw new PlatformException(PlatformError.TodoNotFound);

            todo = request.MapToTodo();
            await _todoContext.SaveChangesAsync(ct);

            var mapped = todo.MapToDto();
            var result = new Result { UpdatedTodo = mapped };
            return result;
        }
    }
}