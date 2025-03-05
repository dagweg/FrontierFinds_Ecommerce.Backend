namespace Ecommerce.Application.UseCases.Users.Common;

public class AuthenticationResult
{
  public required string Id { get; set; }
  public required string FirstName { get; set; }
  public required string LastName { get; set; }
  public required string Email { get; set; }
  public required string Token { get; set; }
}
