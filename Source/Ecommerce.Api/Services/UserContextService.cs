using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Ecommerce.Infrastructure.Services.Authentication;
using FluentResults;
using Microsoft.Extensions.Options;

namespace Ecommerce.Api.Services;

public class UserContextService : IUserContextService
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public UserContextService(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  public Result<string> GetUserId()
  {
    var subId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

    if (subId is null)
      return AuthorizationError.GetResult(nameof(ClaimTypes.NameIdentifier), "Claim not found");

    return Result.Ok(subId);
  }

  public Result<UserId> GetValidUserId()
  {
    var sellerId = GetUserId();
    if (sellerId.IsFailed || Guid.TryParse(sellerId.Value, out var sellerIdGuid) == false)
    {
      return sellerId.ToResult();
    }

    return Result.Ok(UserId.Convert(sellerIdGuid));
  }
}
