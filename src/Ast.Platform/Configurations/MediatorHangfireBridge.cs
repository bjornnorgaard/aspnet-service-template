using MediatR;

namespace Ast.Platform.Configurations;

public class MediatorHangfireBridge
{
    private readonly IMediator _mediator;

    public MediatorHangfireBridge(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Send(IRequest request)
    {
        await _mediator.Send(request);
    }
}