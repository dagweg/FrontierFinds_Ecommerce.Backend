using System.Text.Json;
using Ecommerce.Domain.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Middlewares;

public class DomainExceptionHandlingMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<DomainExceptionHandlingMiddleware> _logger;

  public DomainExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<DomainExceptionHandlingMiddleware> logger
  )
  {
    _next = next;
    _logger = logger;
  }

  public async Task Invoke(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (DomainException ex)
    {
      _logger.LogError(ex, "Domain exception occurred: {ExceptionType}", ex.GetType().Name);

      var exception = ex.GetType();
      var problemDetails = new ProblemDetails
      {
        Type = exception.Name.Replace("Exception", "Failure"),
        Title = exception.Name.Replace("Exception", " Error"),
        Detail = ex.Message,
        Status = StatusCodes.Status400BadRequest,
      };

      context.Response.StatusCode = problemDetails.Status.Value;
      context.Response.ContentType = "application/problem+json";

      await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
    }
  }
}
