using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Domain.UserAggregate.Entities;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.RemoveFromCart;

public class RemoveFromCartCommandHandler
  : IRequestHandler<RemoveFromCartCommand, Result<CartResult>>
{
  private readonly IUserRepository _userRepository;
  private readonly IUserContextService _userContext;
  private readonly IUnitOfWork _unitOfWork;

  public RemoveFromCartCommandHandler(
    IUserRepository userRepository,
    IUserContextService userContext,
    IUnitOfWork unitOfWork
  )
  {
    _userRepository = userRepository;
    _userContext = userContext;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result<CartResult>> Handle(
    RemoveFromCartCommand request,
    CancellationToken cancellationToken
  )
  {
    var userId = _userContext.GetValidUserId();
    if (userId.IsFailed)
      return userId.ToResult();

    var user = await _userRepository.GetByIdAsync(userId.Value);

    if (user is null)
    {
      return NotFoundError.GetResult(nameof(user), "User not found");
    }

    var cartItemIdGuidResult = ConversionUtility.ToGuid(request.CartItemId);

    if (cartItemIdGuidResult.IsFailed)
      return cartItemIdGuidResult.ToResult();

    var cartItemId = CartItemId.Convert(cartItemIdGuidResult.Value);
    var cartItem = user.Cart.GetItem(CartItemId.Convert(cartItemId));

    if (cartItem is null)
      return NotFoundError.GetResult(nameof(cartItem), "Cart item not found");
    user.Cart.RemoveItem(cartItem);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    var cart = await _userRepository.GetCartAsync(userId.Value, null);

    if (cart is null)
    {
      return NotFoundError.GetResult("cart", "Cart not found");
    }

    return Result.Ok(cart);
  }
}
