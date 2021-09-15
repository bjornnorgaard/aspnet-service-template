using System.Threading;
using System.Threading.Tasks;
using Ant.Platform.Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Svc.Todos.Api.Authentication;
using Svc.Todos.Api.Features.Todos;

namespace Svc.Todos.Api.Controllers
{
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Routes.Todos.GetTodo)]
        [Authorize("read:todo")]
        public async Task<GetTodo.Result> GetTodo(
            [FromBody] GetTodo.Command command,
            CancellationToken ct)
        {
            command.UserId = User.GetUserId();
            return await _mediator.Send(command, ct);
        }

        [HttpPost(Routes.Todos.GetTodos)]
        [Authorize("read:todo")]
        public async Task<GetTodos.Result> GetTodos(
            [FromBody] GetTodos.Command command,
            CancellationToken ct)
        {
            command.UserId = User.GetUserId();
            return await _mediator.Send(command, ct);
        }

        [HttpPost(Routes.Todos.CreateTodo)]
        [Authorize("create:todo")]
        public async Task<CreateTodo.Result> CreateTodo(
            [FromBody] CreateTodo.Command command,
            CancellationToken ct)
        {
            command.UserId = User.GetUserId();
            return await _mediator.Send(command, ct);
        }

        [HttpPost(Routes.Todos.UpdateTodo)]
        [Authorize("update:todo")]
        public async Task<UpdateTodo.Result> UpdateTodo(
            [FromBody] UpdateTodo.Command command,
            CancellationToken ct)
        {
            command.UserId = User.GetUserId();
            return await _mediator.Send(command, ct);
        }

        [HttpPost(Routes.Todos.DeleteTodo)]
        [Authorize("update:todo")]
        public AcceptedResult DeleteTodo(
            [FromBody] DeleteTodo.Command command)
        {
            command.UserId = User.GetUserId();
            _mediator.Enqueue(command);
            return Accepted();
        }
    }
}