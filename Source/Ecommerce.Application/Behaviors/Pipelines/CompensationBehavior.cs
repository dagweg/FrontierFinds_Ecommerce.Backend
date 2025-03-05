using Ecommerce.Application.Common.Interfaces.Storage;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.Behaviors.Pipelines;

/// <summary>
///  Pipeline behavior that executes external resource rollback in case of a failed response .
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class CompensationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
{
  private readonly IEnumerable<IExternalResourceTracker> _externalResourceTrackers;

  public CompensationBehavior(IEnumerable<IExternalResourceTracker> externalResourceTrackers)
  {
    _externalResourceTrackers = externalResourceTrackers;
  }

  public async Task<TResponse> Handle(
    TRequest request,
    RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken
  )
  {
    var response = await next();

    // Check if the response is a FluentResults.Result and it indicates failure.
    if (response is Result result && result.IsFailed)
    {
      // Execute external resource rollback for each tracker.
      foreach (var _externalResourceTracker in _externalResourceTrackers)
        await _externalResourceTracker.RollbackAsync();
    }

    return response;
  }
}
