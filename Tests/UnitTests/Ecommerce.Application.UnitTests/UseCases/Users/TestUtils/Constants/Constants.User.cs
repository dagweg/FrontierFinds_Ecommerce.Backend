namespace Ecommerce.Application.UnitTests.UseCases.Users.TestUtils.Constants;

public partial class Constants
{
  public record User
  {
    public const string FirstName = "John";
    public const string LastName = "Doe";
    public const string Email = "johndoe@gmail.com";
    public const string Password = "abcd1234!@#$ABCD";
    public const string ConfirmPassword = "abcd1234!@#$ABCD";
    public const string PhoneNumber = "0911223344";
    public const string CountryCode = "251";
  }
}
