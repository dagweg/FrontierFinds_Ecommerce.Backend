using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.EfCore.Configurations;

internal sealed class ProductCategoryConfigurations : IEntityTypeConfiguration<ProductCategory>
{
  public void Configure(EntityTypeBuilder<ProductCategory> builder)
  {
    builder.ToTable("ProductCategories");
    builder.HasKey(p => p.Id);

    builder.Property(p => p.Id).ValueGeneratedNever().IsRequired();

    builder.OwnsOne(
      p => p.Name,
      pb =>
        pb.Property(p => p.Value)
          .HasConversion(p => p, p => ProductCategoryName.Create(p))
          .HasColumnName("Name")
          .IsRequired()
          .HasMaxLength(255)
    );
  }
}
