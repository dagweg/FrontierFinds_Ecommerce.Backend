using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Infrastructure.Services.Authentication;
using Microsoft.Extensions.Options;

namespace Ecommerce.Api.Services;

public class UserContextService : IUserContextService
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public UserContextService(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  public string? GetUserId()
  {
    var subId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

    return subId;
  }
}
