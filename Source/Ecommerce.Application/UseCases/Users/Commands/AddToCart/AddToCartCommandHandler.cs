using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Interfaces.Validation;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Users.Commands.AddToCart;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.Entities;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.AddToCart;

public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, Result>
{
  private readonly IUserRepository _userRepository;
  private readonly IUserContextService _userContextService;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IProductRepository _productRepository;

  public AddToCartCommandHandler(
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

  public async Task<Result> Handle(AddToCartCommand command, CancellationToken cancellationToken)
  {
    var userIdResult = _userContextService.GetValidUserId();
    if (userIdResult.IsFailed)
      return userIdResult.ToResult();

    List<CartItem> cartItems = [];

    // convert each cartItemCommand to CartItem instance and append to cartItems list
    foreach (var cartItemCommand in command.createCartItemCommands)
    {
      var productIdGuidResult = ConversionUtility.ToGuid(cartItemCommand.ProductId);
      if (productIdGuidResult.IsFailed)
        return productIdGuidResult.ToResult();

      var productId = ProductId.Convert(productIdGuidResult.Value);

      if (!await _productRepository.AnyAsync(productId))
      {
        return NotFoundError.GetResult(nameof(productId), "Product not found");
      }

      var cartItem = CartItem.Create(productId, cartItemCommand.Quantity);

      cartItems.Add(cartItem);
    }

    // add to the user cart
    var result = await _userRepository.AddToCartRangeAsync(userIdResult.Value, cartItems);

    if (result.IsFailed)
      return result;

    // persist to db
    await _unitOfWork.SaveChangesAsync();

    return Result.Ok();
  }
}
