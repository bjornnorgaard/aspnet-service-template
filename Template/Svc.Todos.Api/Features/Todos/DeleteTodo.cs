using System;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Ant.Platform.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Svc.Todos.Api.Database;

namespace Svc.Todos.Api.Features.Todos
{
    public class DeleteTodo
    {
        public class Command : IRequest
        {
            public Guid TodoId { get; set; }
            [JsonIgnore] public string UserId { get; set; }
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
            private readonly TodoContext _todoContext;

            public Handler(TodoContext todoContext)
            {
                _todoContext = todoContext;
            }

            protected override async Task Handle(Command request, CancellationToken ct)
            {
                var todo = await _todoContext.Todos.AsTracking()
                    .Where(t => t.Id == request.TodoId)
                    .Where(t => t.UserId == request.UserId)
                    .FirstOrDefaultAsync(ct);

                if (todo == null) throw new PlatformException(PlatformError.TodoNotFound);

                _todoContext.Todos.Remove(todo);
                await _todoContext.SaveChangesAsync(ct);
            }
        }
    }
}