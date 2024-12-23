namespace Ecommerce.Tests.Presentation.Api;

using Ecommerce.Presentation.Api.Controllers;
using Ecommerce.Presentation.Contracts.Authentication;
using Xunit.Abstractions;

public class AuthenticationControllerTests(
  ITestOutputHelper testOutputHelper,
  AuthenticationController authenticationController
)
{
  private readonly AuthenticationController _authenticationController = authenticationController;
  private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

  [Fact]
  private async Task Register_WithValidRequest_ReturnsOk()
  {
    var registerRequest = new RegisterRequest(
      "firstName",
      "lastName",
      "email",
      "password",
      "phoneNumber",
      1
    );
    var actionResult = await _authenticationController.Register(registerRequest);

    _testOutputHelper.WriteLine(actionResult.ToString());
  }
}
