using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.CodeAnalysis.CSharp;

namespace Ecommerce.Application.UseCases.Users.Commands.WishlistProducts;

public class WishlistProductsCommandHandler : IRequestHandler<WishlistProductsCommand, Result>
{
  private readonly IUserRepository _userRepository;
  private readonly IUserContextService _userContextService;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IProductRepository _productRepository;

  public WishlistProductsCommandHandler(
    IUserRepository userRepository,
    IUserContextService userContextService,
    IUnitOfWork unitOfWork,
    IProductRepository productRepository
  )
  {
    _userRepository = userRepository;
    _userContextService = userContextService;
    _unitOfWork = unitOfWork;
    _productRepository = productRepository;
  }

  public async Task<Result> Handle(
    WishlistProductsCommand request,
    CancellationToken cancellationToken
  )
  {
    var userIdResult = _userContextService.GetValidUserId();
    if (userIdResult.IsFailed)
      return userIdResult.ToResult();

    Result<List<ProductId>> productIdsR = ConversionUtility.ToProductIds(request.ProductIds);

    if (productIdsR.IsFailed)
      return productIdsR.ToResult();

    var success = await _userRepository.AddToWishlistRangeAsync(
      userIdResult.Value,
      productIdsR.Value
    );

    if (success)
    {
      await _unitOfWork.SaveChangesAsync();
      return Result.Ok();
    }

    return Result.Fail("Failed to add products to wishlist");
  }
}
