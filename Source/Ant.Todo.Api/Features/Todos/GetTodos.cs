using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ant.Todo.Api.Database;
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
        }
        
        public class Result
        {
            public List<TodoViewModel> Todos { get; set; }
        }
        
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.PageNumber).NotEmpty().GreaterThanOrEqualTo(0);
                RuleFor(c => c.PageSize).NotEmpty().GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
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
                    .OrderBy(t => t.Id)
                    .Skip(request.PageNumber * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(ct);

                var mapped = _mapper.Map<List<TodoViewModel>>(todos);
                var result = new Result {Todos = mapped};

                return result;
            }
        }
    }
}