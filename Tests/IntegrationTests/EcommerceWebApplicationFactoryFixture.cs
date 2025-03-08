using System.Data;
using Ecommerce.Infrastructure.Persistence.EfCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.IntegrationTests;

public class EcommerceWebApplicationFactoryFixture : WebApplicationFactory<Ecommerce.Api.Program>
{
  public EcommerceWebApplicationFactoryFixture()
  {
    Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
  }

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    Console.WriteLine("Environment Variables in ConfigureWebHost:");
    var devEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    if (devEnv != null)
    {
      Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {devEnv}");
    }
    else
    {
      Console.WriteLine("ASPNETCORE_ENVIRONMENT: Not Set");
    }
    Console.WriteLine("---------------------------------------");

    builder.ConfigureServices(services =>
    {
      var descriptor = services.SingleOrDefault(d =>
        d.ServiceType == typeof(DbContextOptions<EfCoreContext>)
      );

      if (descriptor != null)
      {
        services.Remove(descriptor);
      }

      services.AddDbContext<EfCoreContext>(options =>
      {
        // options.UseNpgsql(
        //   "Host=localhost;Database=FrontierFinds_TestDb;Username=postgres;Password=123;Port=5432"
        // );

        options.UseSqlServer(
          "Server=EVOO-EG-LP7\\SQLEXPRESS;Database=FrontierFinds_TestDb;Trusted_Connection=True;TrustServerCertificate=True;"
        );

        // options.UseInMemoryDatabase("FrontierFinds_TestDb");
      });

      var sp = services.BuildServiceProvider();

      using var scope = sp.CreateScope();

      try // Add try-catch block
      {
        var dbContext = scope.ServiceProvider.GetRequiredService<EfCoreContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
        Console.WriteLine("Database EnsureCreated() successful."); // Log success
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Exception during Database EnsureCreated(): {ex}"); // Log exception
        throw; // Re-throw the exception to fail test startup
      }
    });
  }

  public override async ValueTask DisposeAsync()
  {
    // Put any asynchronous teardown/cleanup logic here that needs to run AFTER tests in this class.
    // For example, database deletion, container stopping.
    Console.WriteLine("Starting DisposeAsync...");
    try
    {
      using (var scope = Services.CreateScope()) // Create scope for teardown
      {
        var scopedServices = scope.ServiceProvider;
        var context = scopedServices.GetRequiredService<EfCoreContext>();
        await context.Database.EnsureDeletedAsync(); // Delete database
        Console.WriteLine("\nTest database deleted.\n");
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error during DisposeAsync database deletion: {ex}");
    }

    await base.DisposeAsync();
  }

  public async Task InitializeAsync()
  {
    using var scope = Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<EfCoreContext>();
    await dbContext.Database.EnsureDeletedAsync();
    await dbContext.Database.EnsureCreatedAsync();
    Console.WriteLine("\nTest database Created.\n");
  }
}
