using Ast.Todos.Database;
using Ast.Todos.Database.Extensions;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ast.Todos.Features.Todos;

public class GetTodos
{
    public class Command : IRequest<Result>
    {
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string SortProperty { get; set; } = nameof(TodoDto.Id);
        public SortOrder SortOrder { get; set; } = SortOrder.None;
    }

    public class Result
    {
        public List<TodoDto> Todos { get; set; }
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
        private readonly IMapper _mapper;

        public Handler(TodoContext todoContext, IMapper mapper)
        {
            _todoContext = todoContext;
            _mapper = mapper;
        }

        public async Task<Result> Handle(Command request, CancellationToken ct)
        {
            var todos = await _todoContext.Todos.AsNoTracking()
                .SortBy(TodoSortExpressions.Get(request.SortProperty), request.SortOrder)
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(ct);

            var mapped = _mapper.Map<List<TodoDto>>(todos);
            var result = new Result { Todos = mapped };

            return result;
        }
    }
}
