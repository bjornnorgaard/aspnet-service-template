using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ant.Platform.PipelineBehaviors
{
    public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest> _validator;
        private readonly ILogger<ValidationPipeline<TRequest, TResponse>> _logger;

        public ValidationPipeline(
            ILogger<ValidationPipeline<TRequest, TResponse>> logger,
            IValidator<TRequest> validator = null)
        {
            _logger = logger;
            _validator = validator;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (_validator == null) return await next();

            var result = await _validator.ValidateAsync(request, cancellationToken);

            if (!result.IsValid)
            {
                var featureName = request.GetType().FullName?.Split(".").Last().Split("+").First();

                _logger.LogWarning("Validation failed for {FeatureName} {@FeatureCommand}", featureName, request);
                throw new ValidationException(result.Errors);
            }

            return await next();
        }
    }
}
