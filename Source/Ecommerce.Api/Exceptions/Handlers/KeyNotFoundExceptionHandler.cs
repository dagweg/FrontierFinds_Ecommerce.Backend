using System.Threading;
using System.Threading.Tasks;
using Ecommerce.Application.Common.Utilities;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Api.Exceptions.Handlers;

public class KeyNotFoundExceptionHandler : IExceptionHandler
{
  private readonly ILogger<KeyNotFoundExceptionHandler> _logger;

  public KeyNotFoundExceptionHandler(ILogger<KeyNotFoundExceptionHandler> logger)
  {
    _logger = logger;
  }

  public async ValueTask<bool> TryHandleAsync(
    HttpContext context,
    Exception ex,
    CancellationToken cancellationToken
  )
  {
    if (ex is not KeyNotFoundException)
    {
      // Return false to indicate that this handler did not handle the exception.
      return false;
    }

    var problemDetails = new ProblemDetails
    {
      Status = StatusCodes.Status404NotFound,
      Type = "ResourceNotFound",
      Title = "Resource Not Found",
      Detail = "The requested resource was not found.",
      Extensions = { { "traceId", context.TraceIdentifier } },
    };

    context.Response.StatusCode = StatusCodes.Status404NotFound;
    context.Response.Headers.Add("X-Trace-ID", context.TraceIdentifier);

    await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

    _logger.LogFormattedError(ex, context.TraceIdentifier);

    // Return true to indicate that the exception was handled.
    return true;
  }
}
