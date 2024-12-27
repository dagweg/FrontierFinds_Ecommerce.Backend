using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;

namespace Ecommerce.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
  /// <summary>
  /// Generates a Json Web Token using the HMACSHA256 Algorithm and the provided Data
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="email"></param>
  /// <param name="firstName"></param>
  /// <param name="lastName"></param>
  /// <returns></returns>
  string GenerateToken(UserId userId, Email email, Name firstName, Name lastName);
}
