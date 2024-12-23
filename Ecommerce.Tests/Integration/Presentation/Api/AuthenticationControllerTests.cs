namespace Ecommerce.Tests.Integration.Presentation.Api;

using System.Net;
using System.Net.Http.Json;
using global::Api;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

public class AuthenticationControllerTests(WebApplicationFactory<Program> webApplicationFactory)
{
  private readonly HttpClient _httpClient = webApplicationFactory.CreateClient();

  [Fact]
  public async Task POST_Login_WithValidCredentials_ReturnsOk()
  {
    var response = await _httpClient.PostAsJsonAsync<object>(
      "/auth/login",
      new { email = "sample@gmail.com", password = "password" }
    );

    response.StatusCode.Should().Be(HttpStatusCode.OK);
  }

  [Fact]
  public async Task POST_Register_WithValidCredentials_ReturnsOk()
  {
    var response = await _httpClient.PostAsJsonAsync<object>(
      "/auth/register",
      new
      {
        firstName = "sampleFirstName",
        lastName = "sampleLastName",
        email = "sample@gmail.com",
        password = "password",
      }
    );

    response.StatusCode.Should().Be(HttpStatusCode.OK);
  }
}
