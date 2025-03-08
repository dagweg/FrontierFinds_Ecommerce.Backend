using Ecommerce.Infrastructure.Persistence.EfCore;
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
  }

  public async Task DisposeAsync()
  {
    await Task.CompletedTask;
  }
}
