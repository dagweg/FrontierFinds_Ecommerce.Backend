using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Users.Commands.RegisterUser;
using Ecommerce.Application.UseCases.Users.Queries.LoginUser;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;

namespace Ecommerce.UnitTests.Ecommerce.Application.UnitTests.UseCases;

public partial class Utils
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
        Name.Create(Utils.User.FirstName),
        Name.Create(Utils.User.LastName),
        Domain.Common.ValueObjects.Email.Create(Utils.User.Email),
        Domain.Common.ValueObjects.Password.Create(Utils.User.Password, Utils.User.Password).Value,
        Domain.Common.ValueObjects.PhoneNumber.Create(Utils.User.PhoneNumber),
        Utils.User.CountryCode
      );
    }

    public static RegisterUserCommand CreateRegisterUserCommand()
    {
      return new RegisterUserCommand(
        Utils.User.FirstName,
        Utils.User.LastName,
        Utils.User.Email,
        Utils.User.Password,
        Utils.User.ConfirmPassword,
        Utils.User.PhoneNumber,
        Utils.User.CountryCode
      );
    }

    public static LoginUserQuery CreateLoginUserQuery()
    {
      return new LoginUserQuery(Utils.User.Email, Utils.User.Password);
    }
  }
}
