using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Contracts.Authentication;
using Ecommerce.Infrastructure.Persistence.EfCore;
using Ecommerce.Tests.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Sdk;

namespace Ecommerce.IntegrationTests.Api;

public class AuthenticationControllerTests : IntegrationTestBase
{
  public AuthenticationControllerTests(EcommerceWebApplicationFactoryFixture factoryFixture)
    : base(factoryFixture) { }

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

  [Fact]
  public async Task LoginShould_ReturnToken_WhenLoginIsSuccessful()
  {
    // arrange
    var registerRequest = Utils.User.CreateRegisterRequest();
    var loginRequest = Utils.User.CreateLoginRequest();

    // act
    await _httpClient.PostAsJsonAsync("auth/register", registerRequest);
    var response = await _httpClient.PostAsJsonAsync("auth/login", loginRequest);
    var content = await response.Content.ReadFromJsonAsync<AuthenticationResult>();

    System.Console.WriteLine(
      JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true })
    );
    System.Console.WriteLine(
      JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })
    );

    // assert
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    content.Should().NotBeNull();
    content.Token.Should().NotBeNull();
  }
}
