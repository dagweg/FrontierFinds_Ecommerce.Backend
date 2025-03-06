using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Authentication;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Localization;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Application.UseCases.Users.Queries.LoginUser;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Ecommerce.UnitTests.Ecommerce.Application.UnitTests.UseCases;
using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;

namespace Ecommerce.Application.UnitTests.UseCases.Users.Queries.LoginUser;

public class LoginUserQueryHandlerTests
{
  private readonly LoginUserQueryHandler _loginUserQueryHandler;
  private readonly Mock<IUserRepository> _userRepositoryMock = new();
  private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock = new();
  private readonly Mock<IAuthenticationMessages> _authValidationMessagesMock = new();
  private readonly Mock<IPasswordHasher<User>> _passwordHasherMock = new();
  private readonly Mock<ILogger<LoginUserQueryHandler>> _loggerMock = new();
  private readonly Mock<IMapper> _mapperMock = new();

  public LoginUserQueryHandlerTests()
  {
    _loginUserQueryHandler = new LoginUserQueryHandler(
      userRespository: _userRepositoryMock.Object,
      jwtTokenGenerator: _jwtTokenGeneratorMock.Object,
      logger: _loggerMock.Object,
      mapper: _mapperMock.Object,
      authValidationMessages: _authValidationMessagesMock.Object,
      passwordHasher: _passwordHasherMock.Object
    );
  }

  [Fact]
  public async Task HandlerShould_ReturnError_WhenUserDoesNotExist()
  {
    // Arrange
    var query = new LoginUserQuery("nonexistent@example.com", "password");
    _userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<Email>())).ReturnsAsync((User)null!);

    _authValidationMessagesMock.Setup(x => x.UserNotFound).Returns("User not found");

    // Act
    var result = await _loginUserQueryHandler.Handle(query, CancellationToken.None);

    // Assert
    result.IsFailed.Should().BeTrue();
    result.Errors.Should().ContainSingle(e => e.Message == "User not found");
  }

  [Fact]
  public async Task HandlerShould_ReturnError_WhenPasswordIsIncorrect()
  {
    // Arrange
    var query = Utils.User.CreateLoginUserQuery();

    _userRepositoryMock
      .Setup(x => x.GetByEmailAsync(It.IsAny<Email>()))
      .ReturnsAsync(Utils.User.Create());

    _passwordHasherMock
      .Setup(x => x.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
      .Returns(PasswordVerificationResult.Failed);

    _authValidationMessagesMock
      .Setup(x => x.EmailOrPasswordIncorrect)
      .Returns("Email or password is incorrect");

    // Act
    var result = await _loginUserQueryHandler.Handle(query, CancellationToken.None);

    // Assert
    result.IsFailed.Should().BeTrue();
    result.Errors.Should().ContainSingle(e => e.Message == "Email or password is incorrect");
  }

  [Fact]
  public async Task HandlerShould_ReturnOk_WhenLoginIsSuccessful()
  {
    // Arrange
    var query = Utils.User.CreateLoginUserQuery();

    _userRepositoryMock
      .Setup(x => x.GetByEmailAsync(It.IsAny<Email>()))
      .ReturnsAsync(Utils.User.Create());

    _passwordHasherMock
      .Setup(x => x.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
      .Returns(PasswordVerificationResult.Success);

    _jwtTokenGeneratorMock
      .Setup(x =>
        x.GenerateToken(It.IsAny<UserId>(), It.IsAny<Email>(), It.IsAny<Name>(), It.IsAny<Name>())
      )
      .Returns(Result.Ok("jwtToken"));

    _mapperMock
      .Setup(x => x.Map<AuthenticationResult>(It.IsAny<User>()))
      .Returns(
        new AuthenticationResult
        {
          Email = query.Email,
          Token = "jwtToken",
          Id = Utils.User.Id,
          FirstName = Utils.User.FirstName,
          LastName = Utils.User.LastName,
        }
      );

    // Act
    var result = await _loginUserQueryHandler.Handle(query, CancellationToken.None);

    // Assert
    result.IsSuccess.Should().BeTrue();
    result.Value.Token.Should().Be("jwtToken");
  }
}
