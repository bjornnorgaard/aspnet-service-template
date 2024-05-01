using Ant.Platform.Hangfire;
using Api.Todos.Features.Todos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Todos.Controllers;

[ApiController]
public class TodoController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Routes.Todos.GetTodo)]
    public async Task<GetTodo.Result> GetTodo([FromBody] GetTodo.Command command, CancellationToken ct)
    {
        return await _mediator.Send(command, ct);
    }

    [HttpPost(Routes.Todos.GetTodos)]
    public async Task<GetTodos.Result> GetTodos([FromBody] GetTodos.Command command, CancellationToken ct)
    {
        return await _mediator.Send(command, ct);
    }

    [HttpPost(Routes.Todos.CreateTodo)]
    public async Task<CreateTodo.Result> CreateTodo([FromBody] CreateTodo.Command command, CancellationToken ct)
    {
        return await _mediator.Send(command, ct);
    }

    [HttpPost(Routes.Todos.UpdateTodo)]
    public async Task<UpdateTodo.Result> UpdateTodo([FromBody] UpdateTodo.Command command, CancellationToken ct)
    {
        return await _mediator.Send(command, ct);
    }

    [HttpPost(Routes.Todos.DeleteTodo)]
    public AcceptedResult DeleteTodo([FromBody] DeleteTodo.Command command)
    {
        _mediator.Enqueue(command);
        return Accepted();
    }
}