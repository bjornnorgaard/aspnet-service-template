using System.Diagnostics;
using Ast.Platform.Exceptions;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;

namespace Ast.Platform.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        Activity.Current?.RecordException(context.Exception);

        switch (context.Exception)
        {
            case PlatformException e:
                _logger.LogWarning(e, e.Message);
                context.Result = e.ToBadRequestObjectResponse();
                break;
            case ValidationException e:
                Activity.Current?.SetStatus(ActivityStatusCode.Ok, "Validation failed");
                var validationResult = new ValidationResult(e.Errors);
                validationResult.AddToModelState(context.ModelState, null);
                context.Result = new BadRequestObjectResult(context.ModelState);
                break;
            case { } e:
                Activity.Current?.SetStatus(ActivityStatusCode.Error, "Unknown exception thrown");
                var template = "HTTP request threw unhandled exception.";
                _logger.LogError(e, template);
                break;
        }
    }
}