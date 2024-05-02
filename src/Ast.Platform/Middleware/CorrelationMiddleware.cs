using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Ast.Platform.Middleware;

public class CorrelationMiddleware
{
    private readonly ILogger<CorrelationMiddleware> _logger;
    private readonly RequestDelegate _next;

    public CorrelationMiddleware(RequestDelegate next, ILogger<CorrelationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        const string header = "x-correlation-id";

        context.Request.Headers.TryGetValue(header, out var correlationId);
        if (string.IsNullOrWhiteSpace(correlationId)) correlationId = Guid.NewGuid().ToString();

        context.Items.Add("CorrelationId", correlationId);
        context.Response.Headers[header] = correlationId;
        using var scope = _logger.BeginScope("{CorrelationId}", correlationId);

        await _next.Invoke(context);
    }
}