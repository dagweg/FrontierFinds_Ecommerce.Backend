using Ecommerce.Application.Common.Extensions;
using Ecommerce.Domain.Common.Enums;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.ProductAggregate.Enums;
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

    builder.OwnsOne(
      p => p.Images,
      ib =>
      {
        ib.ToTable("ProductImages");
        ib.HasKey(nameof(ProductImages.Id), "ProductId");
        ib.Property(i => i.Id).HasColumnName("ProductImageId").IsRequired();
        ib.OwnsOne(
          i => i.Thumbnail,
          tib =>
          {
            tib.HasIndex(t => t.ObjectIdentifier).IsUnique();
            tib.Property(t => t.Url);
            tib.Property(t => t.ObjectIdentifier);
          }
        );
        ib.OwnsOne(
          i => i.LeftImage,
          lib =>
          {
            lib.HasIndex(l => l.ObjectIdentifier).IsUnique();
            lib.Property(l => l.Url);
            lib.Property(l => l.ObjectIdentifier);
          }
        );
        ib.OwnsOne(
          i => i.RightImage,
          rib =>
          {
            rib.HasIndex(r => r.ObjectIdentifier).IsUnique();
            rib.Property(r => r.Url);
            rib.Property(r => r.ObjectIdentifier);
          }
        );
        ib.OwnsOne(
          i => i.FrontImage,
          fib =>
          {
            fib.HasIndex(f => f.ObjectIdentifier).IsUnique();
            fib.Property(f => f.Url);
            fib.Property(f => f.ObjectIdentifier);
          }
        );
        ib.OwnsOne(
          i => i.BackImage,
          bib =>
          {
            bib.HasIndex(b => b.ObjectIdentifier).IsUnique();
            bib.Property(b => b.Url);
            bib.Property(b => b.ObjectIdentifier);
          }
        );
        ib.OwnsOne(
          i => i.TopImage,
          tib =>
          {
            tib.HasIndex(t => t.ObjectIdentifier).IsUnique();
            tib.Property(t => t.Url);
            tib.Property(t => t.ObjectIdentifier);
          }
        );
        ib.OwnsOne(
          i => i.BottomImage,
          bib =>
          {
            bib.HasIndex(b => b.ObjectIdentifier).IsUnique();
            bib.Property(b => b.Url);
            bib.Property(b => b.ObjectIdentifier);
          }
        );

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
