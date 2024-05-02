using AST.Platform.Configurations;
using Hangfire;
using MediatR;

namespace AST.Platform.Hangfire;

public static class HangfireMediatorExtensions
{
    public static void Enqueue(this IMediator mediator, IRequest request)
    {
        var backgroundJobClient = new BackgroundJobClient();
        backgroundJobClient.Enqueue<MediatorHangfireBridge>(x => x.Send(request));
    }
}