using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Ant.Todo.Api.Database;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ant.Todo.Api.Features.Todos
{
    public class CreateTodo
    {
        public class Command : IRequest<Result>
        {
            public string Title { get; set; }
            public string Description { get; set; }
        }
        
        public class Result
        {
            public Guid TodoId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Title).NotEmpty().MinimumLength(2).MaximumLength(25);
                RuleFor(c => c.Description).MaximumLength(100);
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
                var todo = _mapper.Map<Database.Models.Todo>(request);

                await _context.Todos.AddAsync(todo, ct);
                await _context.SaveChangesAsync(ct);

                var result = new Result{TodoId = todo.Id};
                return result;
            }
        }
    }
}