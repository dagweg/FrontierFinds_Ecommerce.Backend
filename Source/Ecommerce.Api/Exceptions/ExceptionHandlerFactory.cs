using Ecommerce.Api.Exceptions.Handlers;
using Microsoft.AspNetCore.Diagnostics;

namespace Ecommerce.Api.Exceptions;

public class ExceptionHandlerFactory
{
  private readonly Dictionary<Type, IExceptionHandler> _handlers;

  public ExceptionHandlerFactory(
    KeyNotFoundExceptionHandler keyNotFoundExceptionHandler,
    UnauthorizedAccessExceptionHandler unauthorizedAccessExceptionHandler,
    GenericExceptionHandler genericExceptionHandler
  )
  {
    _handlers = new Dictionary<Type, IExceptionHandler>
    {
      { typeof(KeyNotFoundException), keyNotFoundExceptionHandler },
      { typeof(UnauthorizedAccessException), unauthorizedAccessExceptionHandler },
    };

    // Default handler for generic exceptions
    _handlers[typeof(Exception)] = genericExceptionHandler;
  }

  public IExceptionHandler GetHandler(Exception ex)
  {
    if (_handlers.TryGetValue(ex.GetType(), out var handler))
    {
      return handler;
    }

    return _handlers[typeof(Exception)];
  }
}
