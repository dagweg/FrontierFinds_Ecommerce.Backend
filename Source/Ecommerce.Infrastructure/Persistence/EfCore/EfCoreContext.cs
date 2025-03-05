using Ecommerce.Domain.OrderAggregate;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Infrastructure.Common;
using Ecommerce.Infrastructure.Common.Providers;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Persistence.EfCore;

public class EfCoreContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
    public DbSet<Promotion> Promotions { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;

    public EfCoreContext() { }

    public EfCoreContext(DbContextOptions<EfCoreContext> options)
      : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Loads in the configurations that implement the IEntityTypeConfiguration and others
        builder.ApplyConfigurationsFromAssembly(InfrastructureAssembly.Assembly);
    }
}
