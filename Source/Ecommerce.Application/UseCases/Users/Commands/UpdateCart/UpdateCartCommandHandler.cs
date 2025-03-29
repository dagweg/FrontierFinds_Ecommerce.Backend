using AutoMapper;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Interfaces.Validation;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Users.Commands.AddToCart;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.Entities;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;

namespace Ecommerce.Application.UseCases.Users.Commands.UpdateCart;

public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, Result<CartResult>>
{
  private readonly IUserRepository _userRepository;
  private readonly IUserContextService _userContextService;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IProductRepository _productRepository;
  private readonly IMapper _mapper;

  public UpdateCartCommandHandler(
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
    UpdateCartCommand command,
    CancellationToken cancellationToken
  )
  {
    var userId = _userContextService.GetValidUserId();
    if (userId.IsFailed)
      return userId.ToResult();

    var user = await _userRepository.GetByIdAsync(userId.Value);

    if (user is null)
    {
      return NotFoundError.GetResult(nameof(User), "User not found");
    }

    var cartItemIdGuidResult = ConversionUtility.ToGuid(command.CartItem.CartItemId);

    if (!cartItemIdGuidResult.IsSuccess)
    {
      return cartItemIdGuidResult.ToResult();
    }

    var cartItemId = CartItemId.Convert(cartItemIdGuidResult.Value);

    CartItem? cartItem = user.Cart.Items.FirstOrDefault(i => i.Id == cartItemId);

    if (cartItem == null)
    {
      return NotFoundError.GetResult(nameof(cartItemId), "Cart item not found");
    }

    var product = await _productRepository.GetByIdAsync(cartItem.ProductId);

    if (product is null)
    {
      return NotFoundError.GetResult(nameof(cartItem.ProductId), "Product not found");
    }

    cartItem.SetQuantity(command.CartItem.Quantity, product.Stock.Quantity);
    cartItem.SetSeen(command.CartItem.Seen);

    user.Cart.UpdateCartItem(cartItem);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    var cartResult = await _userRepository.GetCartAsync(userId.Value, null);
    if (cartResult is null)
    {
      return NotFoundError.GetResult(nameof(cartResult), "Cart not found");
    }

    return Result.Ok(cartResult);
  }
}
