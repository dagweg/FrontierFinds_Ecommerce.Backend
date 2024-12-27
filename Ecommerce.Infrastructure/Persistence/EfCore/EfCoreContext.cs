using Ecommerce.Domain.UserAggregate;
using Ecommerce.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Persistence.EfCore;

public class EfCoreContext : DbContext
{
  public DbSet<User> Users { get; set; } = null!;

  public EfCoreContext(DbContextOptions<EfCoreContext> options)
    : base(options) { }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // Loads in the configurations that implement the IEntityTypeConfiguration and others
    modelBuilder.ApplyConfigurationsFromAssembly(InfrastructureAssembly.Assembly);
  }
}
