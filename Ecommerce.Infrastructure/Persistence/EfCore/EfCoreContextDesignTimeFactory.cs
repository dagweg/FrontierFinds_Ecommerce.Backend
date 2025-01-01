using Ecommerce.Infrastructure.Persistence.EfCore.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure.Persistence.EfCore;

public class EfCoreContextDesignTimeFactory : IDesignTimeDbContextFactory<EfCoreContext>
{
  public EfCoreContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<EfCoreContext>();

    var configuration = new ConfigurationBuilder().Build();

    optionsBuilder.UseSqlServer(
      "Server=EVOO-EG-LP7\\SQLEXPRESS;Database=ecommerce;Trusted_Connection=True;TrustServerCertificate=True;"
    );

    return new EfCoreContext(optionsBuilder.Options);
  }
}
