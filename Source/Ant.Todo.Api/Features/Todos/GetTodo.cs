using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ant.Platform.Exceptions;
using Ant.Todo.Api.Database;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ant.Todo.Api.Features.Todos
{
    public class GetTodo
    {
        public class Command : IRequest<Result>
        {
            public Guid TodoId { get; set; }
        }
        
        public class Result
        {
            public TodoViewModel TodoViewModel { get; set; }
        }
        
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.TodoId).NotEmpty();
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
                var todo = await _context.Todos.AsNoTracking()
                    .Where(t => t.Id == request.TodoId)
                    .FirstOrDefaultAsync(ct);

                if (todo == null) throw new NotFoundException();

                var mapped = _mapper.Map<TodoViewModel>(todo);
                var result = new Result{ TodoViewModel = mapped};
                
                return result;
            }
        }
    }
}