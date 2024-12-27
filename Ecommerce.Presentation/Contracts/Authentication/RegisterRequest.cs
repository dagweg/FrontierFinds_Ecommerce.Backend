namespace Ecommerce.Presentation.Contracts.Authentication;

public record RegisterRequest(
  string FirstName,
  string LastName,
  string Email,
  string Password,
  string ConfirmPassword,
  string PhoneNumber,
  int CountryCode
);
