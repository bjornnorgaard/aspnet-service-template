using System.Diagnostics;
using Ast.Platform.Telemetry;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ast.Platform.PipelineBehaviors;

public class LoggingPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingPipeline<TRequest, TResponse>> _logger;

    public LoggingPipeline(ILogger<LoggingPipeline<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest req, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        var feature = req?.GetType().FullName?.Split(".").Last().Split("+").First();

        using var activity = TelemetrySource.Source.StartActivity(feature);
        Activity.Current?.AddEvent(new ActivityEvent("Feature started"));
        Activity.Current?.AddTag("feature.name", feature);

        _logger.LogInformation("Beginning {FeatureName} {@FeatureCommand}", feature, req);

        var result = await next();

        _logger.LogInformation("Completed {FeatureName} {@FeatureResult}", feature, result);
        Activity.Current?.AddEvent(new ActivityEvent("Feature completed"));

        return result;
    }
}