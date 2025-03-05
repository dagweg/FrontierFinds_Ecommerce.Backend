using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Localization;
using Ecommerce.Application.Common.Interfaces.Validation;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using FluentResults;

namespace Ecommerce.Application.Services.Validation;

public class ProductValidationService : IProductValidationService
{
    private readonly IProductRepository _productRepository;
    private readonly IValidationMessages _validationMessages;

    public ProductValidationService(
      IProductRepository productRepository,
      IValidationMessages validationMessages
    )
    {
        _productRepository = productRepository;
        _validationMessages = validationMessages;
    }

    public async Task<Result> CheckIfProductExistsAsync(ProductId productId)
    {
        if (await _productRepository.AnyAsync(productId))
        {
            return Result.Ok();
        }

        return NotFoundError.GetResult(nameof(productId), _validationMessages.ProductNotFound);
    }
}
