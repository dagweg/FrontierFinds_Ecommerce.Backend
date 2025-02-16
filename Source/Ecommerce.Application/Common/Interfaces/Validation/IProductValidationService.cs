using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using FluentResults;

namespace Ecommerce.Application.Common.Interfaces.Validation;

public interface IProductValidationService
{
  /// <summary>
  /// Check if the product exists
  /// </summary>
  /// <param name="productId"></param>
  /// <returns>
  ///   Ok if the product exists, otherwise a Fail result
  /// </returns>
  Task<Result> CheckIfProductExistsAsync(ProductId productId);
}
