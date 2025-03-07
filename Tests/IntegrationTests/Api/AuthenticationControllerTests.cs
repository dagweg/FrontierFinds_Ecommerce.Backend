using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Ecommerce.Contracts.Authentication;
using Ecommerce.IntegrationTests.Utils.Fixtures;
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
    System.Console.WriteLine(response);

    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

    var content = await response.Content.ReadFromJsonAsync<ProblemDetails>();
    content.Should().NotBeNull();

    // Assert specific error details
    content!.Type.Should().Be("ValidationException"); // Use null forgiving operator as we asserted it's not null
    content!.Title.Should().Be("Validation Error");
    content!.Status.Should().Be(400);
    content!.Detail.Should().Be("One or more validation errors has occured.");

    content!.Extensions["errors"].Should().NotBeNull();

    var errors = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(
      JsonSerializer.Serialize(content!.Extensions["errors"]),
      new JsonSerializerOptions { PropertyNameCaseInsensitive = true, WriteIndented = true }
    );

    if (errors is null)
    {
      throw new XunitException("Errors should not be null.");
    }

    errors.Should().ContainKey("FirstName");
    errors["FirstName"].Should().Contain("First Name is Required");
    errors.Should().ContainKey("LastName");
    errors["LastName"].Should().Contain("Last Name is Required");
    errors.Should().ContainKey("Email");
    errors["Email"].Should().Contain("Email is required.");
    errors["Email"].Should().Contain("Email must be a valid format.");
    errors.Should().ContainKey("Password");
    errors["Password"].Should().Contain("Password is required.");
    errors.Should().ContainKey("ConfirmPassword");
    errors["ConfirmPassword"].Should().Contain("Confirmation password is required.");
    errors.Should().ContainKey("PhoneNumber");
    errors["PhoneNumber"].Should().Contain("Phone number is required.");

    System.Console.WriteLine(
      JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })
    );
  }
}
