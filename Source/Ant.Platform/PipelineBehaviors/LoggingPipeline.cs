using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ant.Platform.PipelineBehaviors
{
    public class LoggingPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingPipeline<TRequest, TResponse>> _logger;

        public LoggingPipeline(ILogger<LoggingPipeline<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest req, CancellationToken ct, RequestHandlerDelegate<TResponse> next)
        {
            var sw = Stopwatch.StartNew();
            var featureName = req.GetType().FullName?.Split(".").Last().Split("+").First();

            try
            {
                var template = "Beginning {FeatureName} {@FeatureCommand}";
                _logger.LogInformation(template, featureName, req);

                var result = await next();

                template = "Completed {FeatureName} {@FeatureResult} in {FeatureElapsedMilliseconds} ms";
                _logger.LogInformation(template, featureName, result, sw.ElapsedMilliseconds);

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Feature threw {UnknownException}", e.GetType());
                throw;
            }
        }
    }
}