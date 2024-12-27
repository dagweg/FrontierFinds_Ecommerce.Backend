using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.Middlewares;

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
        Detail = "An internal server error has occured.",
      };

      context.Response.StatusCode = StatusCodes.Status500InternalServerError;

      await context.Response.WriteAsJsonAsync(problemDetails);

      _logger.LogCritical("An internal server error has occured. {@ex}", ex);
    }
  }
}
