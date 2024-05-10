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
        var sw = Stopwatch.StartNew();
        var feature = req?.GetType().FullName?.Split(".").Last().Split("+").First();
        using var activity = TelemetryConfig.Source.StartActivity(feature);

        var template = "Beginning {FeatureName} {@FeatureCommand}";
        Activity.Current?.AddEvent(new ActivityEvent("Feature started"));
        _logger.LogInformation(template, feature, req);

        var result = await next();

        template = "Completed {FeatureName} {@FeatureResult} in {FeatureElapsedMilliseconds} ms";
        _logger.LogInformation(template, feature, result, sw.ElapsedMilliseconds);
        Activity.Current?.AddEvent(new ActivityEvent("Feature completed"));

        return result;
    }
}