using System.Threading.Tasks;
using Ecommerce.Application.Common.Interfaces.Authentication;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Localization;
using Ecommerce.Application.Common.Interfaces.Validation;
using Ecommerce.Application.UseCases.Users.Commands.RegisterUser;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Ecommerce.UnitTests;
using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Ecommerce.UnitTests.Application.UseCases.Users.Commands.RegisterUser;

public class RegisterUserCommandHandlerTests
{
  private readonly RegisterUserCommandHandler _registerUserCommandHandler;
  private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
  private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
  private readonly Mock<IAuthenticationMessages> _authValidationMessagesMock =
    new Mock<IAuthenticationMessages>();
  private readonly Mock<IUserValidationService> _userValidationServiceMock =
    new Mock<IUserValidationService>();
  private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
  private readonly Mock<IPasswordHasher<User>> _passwordHasherMock =
    new Mock<IPasswordHasher<User>>();

  public RegisterUserCommandHandlerTests()
  {
    _registerUserCommandHandler = new RegisterUserCommandHandler(
      userRespository: _userRepositoryMock.Object,
      jwtTokenGenerator: _jwtTokenGeneratorMock.Object,
      authValidationMessages: _authValidationMessagesMock.Object,
      userValidationService: _userValidationServiceMock.Object,
      unitOfWork: _unitOfWorkMock.Object,
      passwordHasher: _passwordHasherMock.Object
    );
  }

  [Fact]
  public async Task HandlerShould_ReturnError_WhenEmailValidationFails()
  {
    // Arrange
    var command = Utils.User.CreateRegisterUserCommand();

    _userValidationServiceMock
      .Setup(x => x.CheckIfUserAlreadyExistsAsync(It.IsAny<Email>()))
      .ReturnsAsync(Result.Fail("Email is invalid"));

    // Act
    var result = await _registerUserCommandHandler.Handle(command, CancellationToken.None);

    // Assert
    result.IsFailed.Should().BeTrue();
  }

  [Fact]
  public async Task HandlerShould_ReturnOk_WhenRegisterIsSuccessful()
  {
    // Arrange
    var command = Utils.User.CreateRegisterUserCommand();

    _userValidationServiceMock
      .Setup(x => x.CheckIfUserAlreadyExistsAsync(It.IsAny<Email>()))
      .ReturnsAsync(Result.Ok());

    _userValidationServiceMock
      .Setup(x => x.CheckIfUserAlreadyExistsAsync(It.IsAny<PhoneNumber>()))
      .ReturnsAsync(Result.Ok());

    _passwordHasherMock
      .Setup(x => x.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
      .Returns("hashedPassword");

    _userRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>())).ReturnsAsync(true);

    _unitOfWorkMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

    _jwtTokenGeneratorMock
      .Setup(x =>
        x.GenerateToken(It.IsAny<UserId>(), It.IsAny<Email>(), It.IsAny<Name>(), It.IsAny<Name>())
      )
      .Returns(Result.Ok("jwtToken"));

    // Act
    var result = await _registerUserCommandHandler.Handle(command, CancellationToken.None);

    // Assert
    result.IsSuccess.Should().BeTrue();
  }
}
