using System.Diagnostics;
using Ast.Platform.Telemetry;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ast.Platform.PipelineBehaviors;

public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ValidationPipeline<TRequest, TResponse>> _logger;
    private readonly IValidator<TRequest> _validator;

    public ValidationPipeline(
        ILogger<ValidationPipeline<TRequest, TResponse>> logger,
        IValidator<TRequest> validator = null)
    {
        _logger = logger;
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest req, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        using var activity = TelemetrySource.Source.StartActivity("Validation");

        if (_validator == null)
        {
            Activity.Current?.AddEvent(new ActivityEvent("Validation skipped"));
            return await next();
        }

        Activity.Current?.AddEvent(new ActivityEvent("Validation started"));
        var result = await _validator.ValidateAsync(req, ct);
        Activity.Current?.AddEvent(new ActivityEvent("Validation completed"));

        if (!result.IsValid)
        {
            Activity.Current?.AddEvent(new ActivityEvent("Validation failed"));
            var featureName = req.GetType().FullName?.Split(".").Last().Split("+").First();
            _logger.LogWarning("Validation failed for {FeatureName} {@FeatureCommand}", featureName, req);
            throw new ValidationException(result.Errors);
        }

        Activity.Current?.AddEvent(new ActivityEvent("Validation passed"));
        return await next();
    }
}