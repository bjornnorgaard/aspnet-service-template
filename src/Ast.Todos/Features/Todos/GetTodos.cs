using Ast.Platform.Telemetry;
using Ast.Todos.Database;
using Ast.Todos.Database.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ast.Todos.Features.Todos;

public class GetTodos
{
    public class Command : IRequest<Result>
    {
        public int PageNumber { get; init; } = 0;
        public int PageSize { get; init; } = 10;
        public string SortProperty { get; init; } = nameof(TodoDto.Id);
        public SortOrder SortOrder { get; init; } = SortOrder.Desc;
    }

    public class Result
    {
        public IEnumerable<TodoDto> Todos { get; init; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.PageNumber)
                .GreaterThanOrEqualTo(0);

            RuleFor(c => c.PageSize).NotEmpty()
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);

            RuleFor(c => c.SortOrder).IsInEnum();
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
            ActivityCurrent.SetTag("page_number", request.PageNumber);
            ActivityCurrent.SetTag("page_size", request.PageSize);

            var todos = await _todoContext.Todos.AsNoTracking()
                .SortBy(TodoSortExpressions.Get(request.SortProperty), request.SortOrder)
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(ct);

            var mapped = todos.TodoDtos();
            var result = new Result { Todos = mapped };

            return result;
        }
    }
}
