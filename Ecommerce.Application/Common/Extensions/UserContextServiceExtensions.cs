using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;

namespace Ecommerce.Application.Common.Extensions;

public static class UserContextServiceExtensions
{
  /// <summary>
  /// Get the current user's ID
  /// </summary>
  /// <param name="userContextService"></param>
  public static Result<UserId> GetValidUserId(this IUserContextService userContextService)
  {
    var sellerId = userContextService.GetUserId();
    if (sellerId.IsFailed || Guid.TryParse(sellerId.Value, out var sellerIdGuid) == false)
    {
      return sellerId.ToResult();
    }

    return Result.Ok(UserId.Convert(sellerIdGuid));
  }
}
