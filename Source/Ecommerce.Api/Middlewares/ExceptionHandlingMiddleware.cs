using System.Reflection.Metadata;
using Ecommerce.Api.Exceptions;
using Ecommerce.Application.Common.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace Ecommerce.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly ExceptionHandlerFactory _exceptionHandlerFactory;

    public ExceptionHandlingMiddleware(
      RequestDelegate next,
      ILogger<ExceptionHandlingMiddleware> logger,
      ExceptionHandlerFactory exceptionHandlerFactory
    )
    {
        _next = next;
        _logger = logger;
        _exceptionHandlerFactory = exceptionHandlerFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await _exceptionHandlerFactory
              .GetHandler(ex)
              .TryHandleAsync(context, ex, CancellationToken.None);
        }
    }
}
