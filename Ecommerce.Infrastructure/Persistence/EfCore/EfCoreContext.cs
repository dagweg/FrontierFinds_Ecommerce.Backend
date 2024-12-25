using Ecommerce.Domain.User;
using Ecommerce.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Persistence.EfCore;

public partial class EfCoreContext(DbContextOptions<EfCoreContext> options) : DbContext(options)
{
  public DbSet<User> Users { get; set; } = null!;

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
