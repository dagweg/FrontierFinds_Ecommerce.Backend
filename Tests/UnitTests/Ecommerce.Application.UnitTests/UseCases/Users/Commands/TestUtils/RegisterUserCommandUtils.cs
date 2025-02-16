using Ecommerce.Application.UnitTests.UseCases.Users.TestUtils.Constants;
using Ecommerce.Application.UseCases.Users.Commands.RegisterUser;

public static class RegisterUserCommandUtils
{
  public static RegisterUserCommand CreateCommand()
  {
    return new RegisterUserCommand(
      Constants.User.FirstName,
      Constants.User.LastName,
      Constants.User.Email,
      Constants.User.Password,
      Constants.User.ConfirmPassword,
      Constants.User.PhoneNumber,
      Constants.User.CountryCode
    );
  }
}
