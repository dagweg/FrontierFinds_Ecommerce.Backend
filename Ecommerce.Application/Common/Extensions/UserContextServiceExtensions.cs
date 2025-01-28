using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;

namespace Ecommerce.Application.Common.Extensions;

public static class userContextServiceExtensions
{
  /// <summary>
  /// Get the current user's ID
  /// </summary>
  /// <param name="userContextService"></param>
  /// <returns>
  ///   A Result of UserId.
  ///   If the user is not logged in, it will return an error
  /// </returns>
  public static Result<UserId> GetValidUserId(this IUserContextService userContextService)
  {
    var sellerId = userContextService.GetUserId();
    if (sellerId == null || Guid.TryParse(sellerId, out var sellerIdGuid) == false)
    {
      return ValidationErrors.AuthenticationError("Cookie", "Not Logged In");
    }

    return Result.Ok(UserId.Convert(sellerIdGuid));
  }
}
