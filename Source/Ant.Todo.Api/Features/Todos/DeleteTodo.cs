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
            public string UserId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.TodoId).NotEmpty();
                RuleFor(c => c.UserId).NotEmpty();
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
                    .Where(t => t.UserId == request.UserId)
                    .FirstOrDefaultAsync(ct);

                if (todo == null) throw new PlatformException(PlatformError.TodoNotFound);

                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync(ct);
            }
        }
    }
}