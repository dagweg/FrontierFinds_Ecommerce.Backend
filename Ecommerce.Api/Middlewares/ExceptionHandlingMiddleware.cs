using Ecommerce.Application.Common.Utilities;
using Ecommerce.Domain.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ExceptionHandlingMiddleware> _logger;

  public ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger
  )
  {
    _next = next;
    _logger = logger;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (Exception ex)
    {
      var problemDetails = new ProblemDetails
      {
        Status = StatusCodes.Status500InternalServerError,
        Type = "InternalServerFaliure",
        Title = "Internal Server Error",
        Detail = "An internal server error has occured. Please try again later or contact support.",
        Extensions = { { "traceId", context.TraceIdentifier } },
      };

      context.Response.StatusCode = StatusCodes.Status500InternalServerError;
      context.Response.Headers.Add("X-Trace-ID", context.TraceIdentifier);

      await context.Response.WriteAsJsonAsync(problemDetails);

      _logger.LogFormattedError(ex, context.TraceIdentifier);
    }
  }
}
