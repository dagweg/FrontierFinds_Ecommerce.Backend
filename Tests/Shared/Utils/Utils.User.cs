using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Users.Commands.RegisterUser;
using Ecommerce.Application.UseCases.Users.Queries.LoginUser;
using Ecommerce.Contracts.Authentication;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ecommerce.Tests.Shared;

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

    public static Ecommerce.Domain.UserAggregate.User Create()
    {
      return Ecommerce.Domain.UserAggregate.User.Create(
        Ecommerce.Domain.UserAggregate.ValueObjects.Name.Create(Utils.User.FirstName),
        Ecommerce.Domain.UserAggregate.ValueObjects.Name.Create(Utils.User.LastName),
        Ecommerce.Domain.Common.ValueObjects.Email.Create(Utils.User.Email),
        Ecommerce
          .Domain.Common.ValueObjects.Password.Create(Utils.User.Password, Utils.User.Password)
          .Value,
        Ecommerce.Domain.Common.ValueObjects.PhoneNumber.Create(Utils.User.PhoneNumber),
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

    public static RegisterRequest CreateRegisterRequest()
    {
      return new RegisterRequest(
        FirstName: FirstName,
        LastName: LastName,
        Email: Email,
        Password: Password,
        ConfirmPassword: Password,
        PhoneNumber: PhoneNumber,
        CountryCode: int.Parse(CountryCode)
      );
    }

    public static LoginRequest CreateLoginRequest()
    {
      return new LoginRequest(Email: Email, Password: Password);
    }
  }
}
