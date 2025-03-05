using System.Threading;
using System.Threading.Tasks;
using Ecommerce.Application.Common.Utilities;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Api.Exceptions.Handlers;

public class GenericExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GenericExceptionHandler> _logger;

    public GenericExceptionHandler(ILogger<GenericExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
      HttpContext context,
      Exception ex,
      CancellationToken cancellationToken
    )
    {
        // This handler will handle all exceptions that are not handled by other handlers.
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "InternalServerFailure",
            Title = "Internal Server Error",
            Detail = "An internal server error has occurred. Please try again later or contact support.",
            Extensions = { { "traceId", context.TraceIdentifier } },
        };

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.Headers.Add("X-Trace-ID", context.TraceIdentifier);

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        _logger.LogFormattedError(ex, context.TraceIdentifier);

        // Return true to indicate that the exception was handled.
        return true;
    }
}
