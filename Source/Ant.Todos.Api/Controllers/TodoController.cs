using System.Threading;
using System.Threading.Tasks;
using Ant.Platform.Hangfire;
using Ant.Todos.Api.Authentication;
using Ant.Todos.Api.Features.Todos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ant.Todos.Api.Controllers
{
    [ApiController]
    [Route("todos")]
    public class TodoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("get-todo")]
        public async Task<GetTodo.Result> GetTodo(
            [FromBody] GetTodo.Command command,
            CancellationToken ct)
        {
            command.UserId = User.GetUserId();
            return await _mediator.Send(command, ct);
        }

        [HttpPost("get-todos")]
        public async Task<GetTodos.Result> GetTodos(
            [FromBody] GetTodos.Command command,
            CancellationToken ct)
        {
            command.UserId = User.GetUserId();
            return await _mediator.Send(command, ct);
        }

        [HttpPost("create-todo")]
        public async Task<CreateTodo.Result> CreateTodo(
            [FromBody] CreateTodo.Command command,
            CancellationToken ct)
        {
            command.UserId = User.GetUserId();
            return await _mediator.Send(command, ct);
        }

        [HttpPost("update-todo")]
        public async Task<UpdateTodo.Result> UpdateTodo(
            [FromBody] UpdateTodo.Command command,
            CancellationToken ct)
        {
            command.UserId = User.GetUserId();
            return await _mediator.Send(command, ct);
        }

        [HttpPost("delete-todo")]
        public AcceptedResult DeleteTodo(
            [FromBody] DeleteTodo.Command command)
        {
            command.UserId = User.GetUserId();
            _mediator.Enqueue(command);
            return Accepted();
        }
    }
}