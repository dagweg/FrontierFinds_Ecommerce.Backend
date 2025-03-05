using System.Security.Claims;

namespace Ecommerce.Api.Utilities;

public class JwtTokenUtility
{
    public static string? GetUserId(ClaimsPrincipal user)
    {
        var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        return userIdClaim?.Value;
    }
}
