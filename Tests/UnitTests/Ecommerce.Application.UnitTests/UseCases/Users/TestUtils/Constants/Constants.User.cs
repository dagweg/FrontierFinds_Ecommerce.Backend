using Ecommerce.Domain.UserAggregate.ValueObjects;

namespace Ecommerce.Application.UnitTests.UseCases.Users.TestUtils;

public partial class Constants
{
  public record User
  {
    public static readonly string Id = Guid.NewGuid().ToString();
    public const string FirstName = "John";
    public const string LastName = "Doe";
    public const string Email = "johndoe@gmail.com";
    public const string Password = "abcd1234!@#$ABCD";
    public const string ConfirmPassword = "abcd1234!@#$ABCD";
    public const string PhoneNumber = "0911223344";
    public const string CountryCode = "251";

    public static Domain.UserAggregate.User Create()
    {
      return Domain.UserAggregate.User.Create(
        Name.Create(Constants.User.FirstName),
        Name.Create(Constants.User.LastName),
        Domain.Common.ValueObjects.Email.Create(Constants.User.Email),
        Domain
          .Common.ValueObjects.Password.Create(Constants.User.Password, Constants.User.Password)
          .Value,
        Domain.Common.ValueObjects.PhoneNumber.Create(Constants.User.PhoneNumber),
        Constants.User.CountryCode
      );
    }
  }
}
