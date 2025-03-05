using Ecommerce.Application.Common.Errors;
using FluentValidation;
using MediatR;

namespace Ecommerce.Application.Behaviors.Pipelines;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
{
  private readonly IEnumerable<IValidator<TRequest>> _validators;

  public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
  {
    _validators = validators;
  }

  public async Task<TResponse> Handle(
    TRequest request,
    RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken
  )
  {
    var context = new ValidationContext<TRequest>(request);

    var validations = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context)));

    var errors = validations
      .Where(v => !v.IsValid)
      .SelectMany(v => v.Errors)
      .Select(v => new ValidationError(v.PropertyName, v.ErrorMessage))
      .ToList();

    if (errors.Count > 0)
    {
      throw new Common.Errors.ValidationException(errors);
    }

    return await next();
  }
}
