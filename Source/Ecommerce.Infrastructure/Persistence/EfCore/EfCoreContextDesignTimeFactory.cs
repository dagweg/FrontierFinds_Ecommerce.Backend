using Ecommerce.Infrastructure.Persistence.EfCore.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Moq;

namespace Ecommerce.Infrastructure.Persistence.EfCore;

public class EfCoreContextDesignTimeFactory : IDesignTimeDbContextFactory<EfCoreContext>
{
  public EfCoreContextDesignTimeFactory() { }

  public EfCoreContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<EfCoreContext>();

    // optionsBuilder.UseNpgsql(
    //   "Host=localhost;Database=ecommerce;Username=postgres;Password=123;Port=5432;"
    // );
    optionsBuilder.UseSqlServer(
      "Server=EVOO-EG-LP7\\SQLEXPRESS;Database=ecommerce;Trusted_Connection=True;TrustServerCertificate=True;"
    );

    var mockWebHostEnv = new Mock<IWebHostEnvironment>();

    mockWebHostEnv.Setup(m => m.EnvironmentName).Returns(Environments.Development);

    return new EfCoreContext(optionsBuilder.Options, mockWebHostEnv.Object);
  }
}
