using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ant.Todo.Api.Database;
using MediatR;

namespace Ant.Todo.Api.Features.Todos
{
    public class DeleteAllCompletedTodos
    {
        public class Command : IRequest
        {
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
                var todos = _context.Todos.Where(t => t.IsCompleted);
                
                _context.Todos.RemoveRange(todos);
                
                await _context.SaveChangesAsync(ct);
            }
        }
    }
}