using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Todos.Controllers;

[ApiController]
public class TokenController : ControllerBase
{
    private readonly IMediator _mediator;

    public TokenController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("hello-world")]
    public ActionResult HelloWorld()
    {
        return Ok("Hello world");
    }
}
