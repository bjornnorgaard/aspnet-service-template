using Hangfire;
using MediatR;
using Svc.Platform.Configurations;

namespace Svc.Platform.Hangfire
{
    public static class HangfireMediatorExtensions
    {
        public static void Enqueue(this IMediator mediator, IRequest request)
        {
            var backgroundJobClient = new BackgroundJobClient();
            backgroundJobClient.Enqueue<MediatorHangfireBridge>(x => x.Send(request));
        }
    }
}