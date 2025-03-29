using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.NotificationAggregate;
using Ecommerce.Domain.OrderAggregate;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.PaymentAggregate;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.Entities;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Ecommerce.Infrastructure.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Ecommerce.Infrastructure.Persistence.EfCore;

public class EfCoreContext : DbContext
{
  public DbSet<User> Users { get; set; } = null!;
  public DbSet<Product> Products { get; set; } = null!;
  public DbSet<ProductTag> ProductTags { get; set; } = null!;
  public DbSet<Category> Categories { get; set; } = null!;
  public DbSet<Promotion> Promotions { get; set; } = null!;
  public DbSet<Order> Orders { get; set; } = null!;
  public DbSet<Payment> Payments { get; set; } = null!;

  private readonly IWebHostEnvironment _env;

  public EfCoreContext(IWebHostEnvironment env)
  {
    _env = env;
  }

  public EfCoreContext(DbContextOptions<EfCoreContext> options, IWebHostEnvironment env)
    : base(options)
  {
    _env = env;
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.EnableSensitiveDataLogging();
    base.OnConfiguring(optionsBuilder);
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.ApplyConfigurationsFromAssembly(InfrastructureAssembly.Assembly);
  }

  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    return base.SaveChangesAsync(cancellationToken);
  }

  public override int SaveChanges()
  {
    return base.SaveChanges();
  }
}
