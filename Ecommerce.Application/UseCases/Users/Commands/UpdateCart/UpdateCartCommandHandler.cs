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
using Microsoft.VisualBasic;

namespace Ecommerce.Application.UseCases.Users.Commands.UpdateCart;

public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, Result>
{
  private readonly IUserRepository _userRepository;
  private readonly IUserContextService _userContextService;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IProductRepository _productRepository;

  public UpdateCartCommandHandler(
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

  public async Task<Result> Handle(UpdateCartCommand command, CancellationToken cancellationToken)
  {
    var userId = _userContextService.GetValidUserId();
    if (userId.IsFailed)
      return userId.ToResult();

    Dictionary<CartItemId, int> cartItems = [];

    var user = await _userRepository.GetByIdAsync(userId.Value);

    if (user is null)
    {
      return NotFoundError.GetResult(nameof(User), "User not found");
    }

    var dbCartItems = user.Cart.Items.Select(i => i.Id.Value).ToHashSet();

    foreach (var cartItemCommand in command.CartItems)
    {
      var cartItemIdGuidResult = ConversionUtility.ToGuid(cartItemCommand.CartItemId);

      if (!cartItemIdGuidResult.IsSuccess)
      {
        return cartItemIdGuidResult.ToResult();
      }

      var cartItemId = CartItemId.Convert(cartItemIdGuidResult.Value);

      if (!dbCartItems.Contains(cartItemIdGuidResult.Value))
      {
        return NotFoundError.GetResult(nameof(cartItemId), "Cart item not found");
      }

      if (cartItems.ContainsKey(cartItemId))
      {
        cartItems[cartItemId] += cartItemCommand.Quantity;
      }
      else
      {
        cartItems.Add(cartItemId, cartItemCommand.Quantity);
      }
    }

    var success = await _userRepository.UpdateCartAsync(userId.Value, cartItems);

    if (success)
    {
      await _unitOfWork.SaveChangesAsync(cancellationToken);
      return Result.Ok();
    }

    return InternalError.GetResult("Internal error", "Couldn't update cart");
  }
}
