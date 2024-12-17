namespace Ecommerce.Application.UseCases.UserManagement.Authentication.Common;

public class AuthenticationResult
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Token { get; set; }
}
