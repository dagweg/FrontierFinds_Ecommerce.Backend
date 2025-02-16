using Microsoft.AspNetCore.Http;

namespace Ecommerce.Infrastructure.Services.Authentication;

public class CookieSettings
{
  public const string SectionName = "CookieSettings";

  public required bool Secure { get; init; }
  public required bool HttpOnly { get; init; }
  public required SameSiteMode SameSite { get; init; }
  public required string CookieKey { get; init; }
}
