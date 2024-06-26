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
        ActivityCurrent.SetTag("feature.name", feature);

        ActivityCurrent.AddEvent("Feature started");
        _logger.LogInformation("Beginning {FeatureName} {@FeatureCommand}", feature, req);

        var result = await next();

        _logger.LogInformation("Completed {FeatureName} {@FeatureResult}", feature, result);
        ActivityCurrent.AddEvent("Feature completed");

        return result;
    }
}
