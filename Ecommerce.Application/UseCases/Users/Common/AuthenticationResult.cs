namespace Ecommerce.Application.UseCases.Users.Common;

public class AuthenticationResult
{
  public required string FirstName { get; init; }
  public required string LastName { get; init; }
  public required string Email { get; init; }
  public required string Token { get; init; }
}
