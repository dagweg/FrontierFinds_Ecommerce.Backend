using System.Threading.Tasks;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.Entities;
using Ecommerce.Domain.UserAggregate.Events;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace Ecommerce.UnitTests.Domain.UserAggregate;

public class UserTests
{
  private readonly User _user;

  public UserTests()
  {
    var passwordHasher = new PasswordHasher<User>();

    _user = User.Create(
      firstName: Name.Create(Utils.User.FirstName),
      lastName: Name.Create(Utils.User.LastName),
      email: Email.Create(Utils.User.Email),
      password: Password.Create("", Utils.User.Password).Value, // unhashed
      phoneNumber: PhoneNumber.Create(Utils.User.PhoneNumber),
      countryCode: Utils.User.CountryCode
    );

    _user.ChangePassword(
      Password
        .Create(passwordHasher.HashPassword(_user, Utils.User.Password), Utils.User.Password)
        .Value
    );
  }

  [Fact]
  public void VerifyEmailShould_ReturnFailed_WhenEmailOtpIsNull()
  {
    // Arrange
    _user.SetEmailVerificationOtp(null);

    // Act
    var result = _user.VerifyEmail(OneTimePassword.CreateNew());

    // Assert
    result.IsFailed.Should().BeTrue();
  }

  [Fact]
  public void VerifyEmailShould_ReturnFailed_WhenEmailOtpIsNotCorrect()
  {
    // Arrange
    var validOtp = OneTimePassword.Convert([1, 2, 3, 4, 5, 6]).Value;
    _user.SetEmailVerificationOtp(validOtp); // make sure email verification otp is set (not null)

    var invalidOtp = OneTimePassword.Convert([1, 2, 3, 4, 5, 7]).Value;
    // Act
    var result = _user.VerifyEmail(invalidOtp);

    // Assert
    result.IsFailed.Should().BeTrue();
  }

  [Fact]
  public void VerifyPasswordResetShould_ReturnFailed_WhenPasswordOtpIsNull()
  {
    // Arrange
    _user.SetPasswordResetOtp(null);

    // Act
    var result = _user.VerifyPasswordResetOtp(OneTimePassword.CreateNew());

    // Assert
    result.IsFailed.Should().BeTrue();
  }

  [Fact]
  public void VerifyPasswordResetShould_ReturnFailed_WhenPasswordIsNotCorrect()
  {
    // Arrange
    var validOtp = OneTimePassword.Convert([1, 2, 3, 4, 5, 6]).Value;
    _user.SetPasswordResetOtp(validOtp); // not null

    var invalidOtp = OneTimePassword.Convert([1, 2, 3, 4, 5, 7]).Value;
    // Act
    var result = _user.VerifyPasswordResetOtp(invalidOtp);

    // Assert
    result.IsFailed.Should().BeTrue();
  }
}
