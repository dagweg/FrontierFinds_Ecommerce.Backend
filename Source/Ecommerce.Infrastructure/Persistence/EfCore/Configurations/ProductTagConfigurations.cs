using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.EfCore.Configurations;

public class ProductTagConfigurations : IEntityTypeConfiguration<ProductTag>
{
  public void Configure(EntityTypeBuilder<ProductTag> builder)
  {
    builder.HasKey(x => x.Id);

    builder.Property(x => x.Id);

    builder.Property(x => x.Name).IsRequired();

    builder.HasIndex(x => x.Name).IsUnique();

    builder.HasMany(x => x.Products).WithMany(x => x.Tags).UsingEntity("ProductTagLink");
  }
}
