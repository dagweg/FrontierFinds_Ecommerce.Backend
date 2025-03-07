using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Ecommerce.Contracts.Authentication;
using Ecommerce.Tests.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit.Sdk;

namespace Ecommerce.IntegrationTests.Api;

public class AuthenticationControllerTests : IClassFixture<EcommerceWebApplicationFactoryFixture>
{
  private readonly HttpClient _httpClient;

  public AuthenticationControllerTests(EcommerceWebApplicationFactoryFixture factoryFixture)
  {
    _httpClient = factoryFixture.CreateClient();
  }

  [Fact]
  public async Task RegisterShould_ReturnCreated_WhenRegistrationIsSuccessful()
  {
    // Arrange
    var registerRequest = Utils.User.CreateRegisterRequest();

    // Act
    var response = await _httpClient.PostAsJsonAsync("auth/register", registerRequest);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.Created);
  }
}
