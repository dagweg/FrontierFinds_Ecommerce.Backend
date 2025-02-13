using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.RemoveFromCart;

public class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand, Result>
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

  public async Task<Result> Handle(
    RemoveFromCartCommand request,
    CancellationToken cancellationToken
  )
  {
    var userId = _userContext.GetValidUserId();
    if (userId.IsFailed)
      return userId.ToResult();

    HashSet<CartItemId> cartItemIds = [];

    foreach (var cartItem in request.CartItemIds)
    {
      var cartItemIdGuidResult = ConversionUtility.ToGuid(cartItem);

      if (cartItemIdGuidResult.IsSuccess)
      {
        var cartItemId = CartItemId.Convert(cartItemIdGuidResult.Value);
        cartItemIds.Add(cartItemId);
      }
    }

    var success = await _userRepository.RemoveFromCartRangeAsync(userId.Value, cartItemIds);

    if (success)
    {
      await _unitOfWork.SaveChangesAsync(cancellationToken);
      return Result.Ok();
    }

    return InternalError.GetResult("Couldn't remove items from cart");
  }
}
