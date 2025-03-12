using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Ecommerce.Contracts.Authentication;
using Ecommerce.Infrastructure.Persistence.EfCore;
using Ecommerce.Tests.Shared;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.IntegrationTests;

public abstract class IntegrationTestBase
  : IClassFixture<EcommerceWebApplicationFactoryFixture>,
    IAsyncLifetime
{
  protected readonly HttpClient _httpClient;
  protected readonly DatabaseFacade _db;
  protected readonly IServiceScope _scope;
  protected readonly IServiceProvider _sp;

  public IntegrationTestBase(EcommerceWebApplicationFactoryFixture fixture)
  {
    _httpClient = fixture.CreateClient();
    _scope = fixture.Services.CreateScope();
    _sp = _scope.ServiceProvider;
    _db = _sp.GetRequiredService<EfCoreContext>().Database;
  }

  public async Task InitializeAsync()
  {
    // makes sure each test method starts with clean slate db to avoid conflicts
    await _db.EnsureDeletedAsync();
    await _db.EnsureCreatedAsync();
    _httpClient.DefaultRequestHeaders.Authorization = null; // reset auth before each test
  }

  public async Task DisposeAsync()
  {
    await Task.CompletedTask;
  }

  public async Task Authenticate()
  {
    await _httpClient.PostAsJsonAsync("auth/register", Utils.User.CreateRegisterRequest());
    var response = await _httpClient.PostAsJsonAsync("auth/login", Utils.User.CreateLoginRequest());

    var authR = JsonSerializer.Deserialize<AuthenticationResponse>(
      await response.Content.ReadAsStreamAsync()
    );

    if (authR is null)
    {
      System.Console.WriteLine("Couldn't authenticate user before test.");
      return;
    }

    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
      "Bearer",
      authR.Token
    );
  }
}
