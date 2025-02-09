using Ecommerce.Application.Common.Extensions;
using Ecommerce.Domain.Common.Enums;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.EfCore.Configurations;

internal sealed class ProductConfigurations : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    builder.ToTable("Products");
    builder.HasKey(p => p.Id);

    // Value Objects
    builder
      .Property(p => p.Id)
      .ValueGeneratedNever()
      .HasConversion(id => id.Value, value => ProductId.Convert(value));

    builder
      .OwnsOne(p => p.Name)
      .Property(n => n.Value)
      .HasConversion(v => v, v => ProductName.Create(v))
      .HasColumnName("Name")
      .IsRequired()
      .HasMaxLength(255);

    builder
      .OwnsOne(p => p.Description)
      .Property(d => d.Value)
      .HasConversion(v => v, v => ProductDescription.Create(v))
      .HasColumnName("Description")
      .IsRequired()
      .HasMaxLength(1000);
    builder.OwnsOne(
      p => p.Price,
      pb =>
      {
        pb.Property(p => p.Value).HasColumnName("PriceValue").IsRequired();
        pb.Property(p => p.Currency)
          .HasConversion(v => v.ToString(), v => v.ConvertTo<Currency>() ?? Currency.None)
          .HasColumnName("PriceCurrency")
          .IsRequired()
          .HasMaxLength(3);
      }
    );
    builder.OwnsOne(
      p => p.Stock,
      sb =>
      {
        sb.OwnsOne(s => s.Quantity)
          .Property(q => q.Value)
          .HasConversion(v => v, v => Quantity.Create(v))
          .HasColumnName("StockQuantity")
          .IsRequired();
        sb.Property(s => s.Reserved).HasColumnName("StockReserved").IsRequired();
      }
    );
    builder
      .Property(p => p.SellerId)
      .HasConversion(v => v.Value, v => UserId.Convert(v))
      .HasColumnName("SellerId")
      .IsRequired();

    // Collections
    // Configure categories many-to-many not-owned relationship
    builder
      .HasMany<ProductCategory>()
      .WithMany()
      .UsingEntity(j => j.ToTable("ProductCategoryLink"));

    // configure tags one-to-many owned relationship
    builder
      .OwnsMany(
        p => p.Tags,
        tb =>
        {
          tb.ToTable("ProductTags");
          tb.HasKey(nameof(ProductTag.Id), "ProductId");
          tb.Property(t => t.Id).HasColumnName("TagId").IsRequired();
          tb.Property(t => t.Name).HasColumnName("TagName").IsRequired().HasMaxLength(255);
          tb.WithOwner().HasForeignKey("ProductId");
        }
      )
      .Metadata.FindNavigation(nameof(Product.Tags))!
      .SetPropertyAccessMode(PropertyAccessMode.Field);

    // configure reviews one-to-many owned relationship
    builder
      .OwnsMany(
        p => p.Reviews,
        rb =>
        {
          rb.ToTable("ProductReviews");
          rb.HasKey(nameof(ProductReview.Id), "ProductId");
          rb.Property(r => r.Id).HasColumnName("ReviewId").IsRequired();
          rb.OwnsOne(
            r => r.AuthorId,
            ab =>
            {
              ab.Property(a => a.Value) // Access the 'Value' property of AuthorId
                .HasConversion(a => a, a => UserId.Convert(a))
                .HasColumnName("AuthorId")
                .IsRequired();
            }
          );

          rb.Property(r => r.Description).IsRequired().HasMaxLength(1000);
          rb.OwnsOne(
            r => r.Rating,
            rb =>
            {
              rb.Property(r => r.Value)
                .HasColumnName("Rating")
                .HasConversion(r => r, r => Rating.Create(r).Value)
                .IsRequired();
            }
          );
          rb.WithOwner().HasForeignKey("ProductId");
        }
      )
      .Metadata.FindNavigation(nameof(Product.Reviews))!
      .SetPropertyAccessMode(PropertyAccessMode.Field);

    // Entities
    builder.OwnsOne(
      p => p.Thumbnail,
      tb =>
      {
        tb.ToTable("ProductThumbnails");
        tb.HasKey(nameof(ProductImage.Id), "ProductId");
        tb.Property(i => i.Id).IsRequired();
        tb.Property(i => i.Url).IsRequired();
        tb.WithOwner().HasForeignKey("ProductId");
      }
    );

    builder.OwnsOne(
      p => p.Images,
      ib =>
      {
        ib.ToTable("ProductImages");
        ib.HasKey(nameof(ProductImages.Id), "ProductId");
        ib.Property(i => i.Id).HasColumnName("ProductImageId").IsRequired();
        ib.Property(i => i.LeftImageUrl);
        ib.Property(i => i.RightImageUrl);
        ib.Property(i => i.FrontImageUrl);
        ib.Property(i => i.BackImageUrl);
        ib.WithOwner().HasForeignKey("ProductId");
      }
    );

    // Value Object
    builder
      .OwnsOne(p => p.AverageRating)
      .Property(r => r.Value)
      .HasConversion(r => r, r => Rating.Create(r).Value)
      .HasColumnName("AverageRating")
      .HasDefaultValue(0);

    // Entity
    builder.OwnsOne(
      p => p.Promotion,
      pb =>
      {
        pb.ToTable("ProductPromotions");
        pb.HasKey(nameof(Promotion.Id), "ProductId");
        pb.Property(p => p.Id).HasColumnName("ProductPromotionId").IsRequired();
        pb.Property(p => p.DiscountPercentage).IsRequired();
        pb.Property(p => p.StartDate).IsRequired();
        pb.Property(p => p.EndDate).IsRequired();
        pb.WithOwner().HasForeignKey("ProductId");
      }
    );
  }
}
