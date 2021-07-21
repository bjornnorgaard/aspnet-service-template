using System.Threading;
using System.Threading.Tasks;
using Ant.Platform.Hangfire;
using Ant.Todo.Api.Features.Todos;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Ant.Todo.Api.Background
{
    public class CleanupBackgroundService : BackgroundService
    {
        private readonly IMediator _mediator;

        public CleanupBackgroundService(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override async Task ExecuteAsync(CancellationToken st)
        {
            while (!st.IsCancellationRequested)
            {
                _mediator.Enqueue(new DeleteAllCompletedTodos.Command());
                await Task.Delay(1000 * 60, st);
            }
        }
    }
}