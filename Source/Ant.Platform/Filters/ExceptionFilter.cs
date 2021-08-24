using System;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Svc.Platform.Exceptions;

namespace Svc.Platform.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case PlatformException e:
                    _logger.LogWarning(e, e.Message);
                    context.Result = e.ToResponseObject();
                    break;
                case ValidationException e:
                    var validationResult = new ValidationResult(e.Errors);
                    validationResult.AddToModelState(context.ModelState, null);
                    context.Result = new BadRequestObjectResult(context.ModelState);
                    break;
                case Exception e:
                    var template = "HTTP request threw unhandled exception.";
                    _logger.LogError(e, template);
                    break;
            }
        }
    }
}