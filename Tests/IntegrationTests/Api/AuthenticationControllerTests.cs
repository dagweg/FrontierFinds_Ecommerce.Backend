using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Ecommerce.Contracts.Authentication;
using Ecommerce.IntegrationTests.Utils.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.IntegrationTests.Api;

public class AuthenticationControllerTests : IClassFixture<EcommerceWebApplicationFactoryFixture>
{
  private readonly HttpClient _httpClient;

  public AuthenticationControllerTests(EcommerceWebApplicationFactoryFixture factoryFixture)
  {
    _httpClient = factoryFixture.CreateClient();
  }

  [Fact]
  public async Task RegisterShould_ReturnErrorValidation_WhenRegisterRequestIsInvalid()
  {
    // Arrange
    var registerRequest = new RegisterRequest(
      string.Empty,
      string.Empty,
      string.Empty,
      string.Empty,
      string.Empty,
      string.Empty,
      0
    );

    // Act
    var response = await _httpClient.PostAsJsonAsync("auth/register", registerRequest);

    // Assert
    Console.WriteLine(response);
    // response.StatusCode.Should().Be();
  }
}
