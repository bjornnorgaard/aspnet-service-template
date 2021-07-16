using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ant.Platform.Exceptions;
using Ant.Todo.Api.Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ant.Todo.Api.Features.Todos
{
    public class DeleteTodo
    {
        public class Command : IRequest
        {
            public Guid TodoId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.TodoId).NotEmpty();
            }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly Context _context;

            public Handler(Context context)
            {
                _context = context;
            }

            protected override async Task Handle(Command request, CancellationToken ct)
            {
                var todo = await _context.Todos.AsTracking()
                    .Where(t => t.Id == request.TodoId)
                    .FirstOrDefaultAsync(ct);

                if (todo == null) throw new NotFoundException();

                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync(ct);
            }
        }
    }
}