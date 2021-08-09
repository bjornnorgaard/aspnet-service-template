using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Ant.Todo.Api.Database;
using Ant.Todo.Api.Database.Configurations;
using Ant.Todo.Api.Database.Extensions;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ant.Todo.Api.Features.Todos
{
    public class GetTodos
    {
        public class Command : IRequest<Result>
        {
            public int PageNumber { get; set; } = 0;
            public int PageSize { get; set; } = 10;
            [JsonIgnore] public string UserId { get; set; }
            public string SortProperty { get; set; } = nameof(TodoViewModel.Id);
            public SortOrder SortOrder { get; set; } = SortOrder.None;
        }

        public class Result
        {
            public List<TodoViewModel> Todos { get; set; }
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

                RuleFor(c => c.UserId).NotEmpty()
                    .MinimumLength(TodoConstants.UserId.MinLenght)
                    .MaximumLength(TodoConstants.UserId.MaxLength);

                RuleFor(c => c.SortOrder).IsInEnum();
            }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly Context _context;
            private readonly IMapper _mapper;

            public Handler(Context context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result> Handle(Command request, CancellationToken ct)
            {
                var todos = await _context.Todos.AsNoTracking()
                    .Where(t => t.UserId == request.UserId)
                    .SortBy(TodoSortExpressions.Get(request.SortProperty), request.SortOrder)
                    .Skip(request.PageNumber * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(ct);

                var mapped = _mapper.Map<List<TodoViewModel>>(todos);
                var result = new Result { Todos = mapped };

                return result;
            }
        }
    }
}