using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Middlewares;

public class FluentValidationExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<FluentValidationExceptionHandlingMiddleware> _logger;

    public FluentValidationExceptionHandlingMiddleware(
      RequestDelegate next,
      ILogger<FluentValidationExceptionHandlingMiddleware> logger
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
        catch (ValidationException ex)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation Error",
                Type = "ValidationFailure",
                Detail = "One or more validation errors has occured.",
            };

            if (ex.Errors is not null)
            {
                problemDetails.Extensions["errors"] = ex
                  .Errors.GroupBy(e => e.Path)
                  .ToDictionary(kvp => kvp.Key, kvp => kvp.Select(e => e.Message).ToList());
            }

            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(problemDetails);
            _logger.LogFormattedError(ex, context.TraceIdentifier);
        }
    }
}
