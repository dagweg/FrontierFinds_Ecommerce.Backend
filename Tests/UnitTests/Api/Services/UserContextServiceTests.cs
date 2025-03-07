using System.Security.Claims;
using Ecommerce.Api.Services;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Ecommerce.UnitTests.Api.Services;

public class UserContextServiceTests
{
  private readonly IUserContextService _userContextService;
  private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock =
    new Mock<IHttpContextAccessor>();

  public UserContextServiceTests()
  {
    _userContextService = new UserContextService(_httpContextAccessorMock.Object);
  }

  [Fact]
  public void GetUserIdShould_ReturnOk_WhenContextHasClaim()
  {
    // arrange
    _httpContextAccessorMock.SetupProperty(
      x => x.HttpContext,
      new DefaultHttpContext
      {
        User = new ClaimsPrincipal(
          new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "name-identifier") })
        ),
      }
    );

    // act
    var result = _userContextService.GetUserId();

    // assert
    result.IsSuccess.Should().BeTrue();
  }

  [Fact]
  public void GetUserIdShould_ReturnAuthorizationError_WhenContextDoesntHaveClaim()
  {
    // arrange
    _httpContextAccessorMock.SetupProperty(
      x => x.HttpContext,
      new DefaultHttpContext { User = new ClaimsPrincipal() }
    );

    // act
    var result = _userContextService.GetUserId();

    // assert
    result.IsFailed.Should().BeTrue();
    result.Errors.Find(x => x.GetType() == typeof(AuthorizationError)).Should().NotBeNull();
  }
}
