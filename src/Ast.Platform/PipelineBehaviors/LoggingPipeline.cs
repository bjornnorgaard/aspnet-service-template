using System.Diagnostics;
using Ast.Platform.Exceptions;
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

    public async Task<TResponse> Handle(TRequest req, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var sw = Stopwatch.StartNew();
        var featureName = req?.GetType().FullName?.Split(".").Last().Split("+").First();

        try
        {
            var template = "Beginning {FeatureName} {@FeatureCommand}";
            _logger.LogInformation(template, featureName, req);

            var result = await next();

            template = "Completed {FeatureName} {@FeatureResult} in {FeatureElapsedMilliseconds} ms";
            _logger.LogInformation(template, featureName, result, sw.ElapsedMilliseconds);

            return result;
        }
        catch (PlatformException)
        {
            // Not an issue.
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Feature threw {UnknownException}", e.GetType());
            throw;
        }
    }
}