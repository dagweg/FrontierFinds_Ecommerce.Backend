using Ecommerce.Application.Common.Interfaces.Providers.Date;
using Ecommerce.Infrastructure.Services.Authentication;
using Ecommerce.Tests.Shared;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Ecommerce.UnitTests.Infrastructure.Services;

public class JwtTokenGeneratorTests
{
  private JwtTokenGenerator _jwtTokenGenerator;

  private Mock<IOptions<JwtSettings>> _jwtSettingsMock;
  private Mock<IDateTimeProvider> _dateTimeProviderMock;
  private Mock<ILogger<JwtTokenGenerator>> _loggerMock;

  // Fixtures
  private readonly IOptions<JwtSettings> _jwtSettingsFixture = new OptionsWrapper<JwtSettings>(
    new JwtSettings
    {
      Audience = Utils.JwtSettings.Audience,
      Issuer = Utils.JwtSettings.Issuer,
      SecretKey = Utils.JwtSettings.SecretKey,
      ExpiryMinutes = Utils.JwtSettings.ExpiryMinutes,
    }
  );

  public JwtTokenGeneratorTests()
  {
    _jwtSettingsMock = new Mock<IOptions<JwtSettings>>();
    _jwtSettingsMock.Setup(x => x.Value).Returns(_jwtSettingsFixture.Value);

    _dateTimeProviderMock = new Mock<IDateTimeProvider>();
    _loggerMock = new Mock<ILogger<JwtTokenGenerator>>();

    _jwtTokenGenerator = new JwtTokenGenerator(
      _jwtSettingsMock.Object,
      _dateTimeProviderMock.Object,
      _loggerMock.Object
    );
  }

  [Fact]
  public void GenerateToken_ReturnsInternalError_WhenSecretKeyIsNull()
  {
    //arrange
    var user = Utils.User.Create();

    _jwtSettingsMock
      .Setup(x => x.Value)
      .Returns(
        new JwtSettings
        {
          Audience = Utils.JwtSettings.Audience,
          Issuer = Utils.JwtSettings.Issuer,
          SecretKey = null!, // Set SecretKey to null
          ExpiryMinutes = Utils.JwtSettings.ExpiryMinutes,
        }
      );

    _jwtTokenGenerator = new JwtTokenGenerator(
      _jwtSettingsMock.Object,
      _dateTimeProviderMock.Object,
      _loggerMock.Object
    );

    // act
    var result = _jwtTokenGenerator.GenerateToken(
      user.Id,
      user.Email,
      user.FirstName,
      user.LastName
    );

    //assert

    result.IsFailed.Should().BeTrue();
  }

  [Fact]
  public void GenerateToken_ShouldReturnOkWithValidToken_WhenValidParametersAreProvided()
  {
    //arrange
    var user = Utils.User.Create();

    // act
    var result = _jwtTokenGenerator.GenerateToken(
      user.Id,
      user.Email,
      user.FirstName,
      user.LastName
    );

    //assert
    result.IsSuccess.Should().BeTrue();
    result.Value.Should().NotBeNullOrEmpty();
  }
}
