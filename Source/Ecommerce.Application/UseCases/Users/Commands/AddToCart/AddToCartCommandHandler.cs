using AutoMapper;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Interfaces.Validation;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Users.Commands.AddToCart;
using Ecommerce.Domain.Common.Errors;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.Entities;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.AddToCart;

public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, Result<CartResult>>
{
  private readonly IUserRepository _userRepository;
  private readonly IUserContextService _userContextService;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IProductRepository _productRepository;
  private readonly IMapper _mapper;

  public AddToCartCommandHandler(
    IUserRepository userRepository,
    IUserContextService userContextService,
    IUnitOfWork unitOfWork,
    IProductRepository productRepository,
    IMapper mapper
  )
  {
    _userRepository = userRepository;
    _userContextService = userContextService;
    _unitOfWork = unitOfWork;
    _productRepository = productRepository;
    _mapper = mapper;
  }

  public async Task<Result<CartResult>> Handle(
    AddToCartCommand command,
    CancellationToken cancellationToken
  )
  {
    var userIdResult = _userContextService.GetValidUserId();
    if (userIdResult.IsFailed)
      return userIdResult.ToResult();

    var user = await _userRepository.GetByIdAsync(userIdResult.Value);

    if (user is null)
      return NotFoundError.GetResult("user", "User not found");

    List<CartItem> cartItems = [];

    // convert each cartItemCommand to CartItem instance and append to cartItems list
    foreach (var cartItemCommand in command.createCartItemCommands)
    {
      var productIdGuidResult = ConversionUtility.ToGuid(cartItemCommand.ProductId);
      if (productIdGuidResult.IsFailed)
        return productIdGuidResult.ToResult();

      var productId = ProductId.Convert(productIdGuidResult.Value);

      var product = await _productRepository.GetByIdAsync(productId);

      if (product == null)
      {
        return NotFoundError.GetResult(nameof(productId), "Product not found");
      }

      if (product.SellerId == userIdResult.Value)
      {
        return InvalidOperationError.GetResult(
          nameof(CartItem),
          "You cannot add you own product to cart."
        );
      }

      var cartItem = CartItem.Create(productId, cartItemCommand.Quantity);

      cartItems.Add(cartItem);
    }

    var productBulk = await _productRepository.BulkGetByIdAsync(
      user.Cart.Items.Select(ci => ci.ProductId)
    );

    var result = user.Cart.AddItemsRange(cartItems, productBulk, userIdResult.Value);

    if (result.IsFailed)
      return result;

    // persist to db
    await _unitOfWork.SaveChangesAsync();

    var cart = await _userRepository.GetCartAsync(userIdResult.Value, null);

    if (cart is null)
    {
      return NotFoundError.GetResult("cart", "Cart not found");
    }

    return Result.Ok(cart);
  }
}
