using Ecommerce.Application.Common.Extensions;
using Ecommerce.Domain.Common.Enums;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.OrderAggregate;
using Ecommerce.Domain.OrderAggregate.Entities;
using Ecommerce.Domain.OrderAggregate.Enums;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.EfCore.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder.ToTable("Orders");

    builder.HasKey(nameof(Order.Id));

    builder
      .Property(o => o.Id)
      .ValueGeneratedNever()
      .HasColumnName("OrderId")
      .HasConversion(id => id.Value, value => OrderId.Convert(value))
      .IsRequired();

    builder.Property(o => o.OrderDate).HasColumnName("OrderDate").IsRequired();

    builder.OwnsOne(
      o => o.Total,
      tb =>
      {
        tb.Property(t => t.Value).HasColumnName("TotalPrice").IsRequired();
        tb.Property(p => p.Currency)
          .HasColumnName("Currency")
          .IsRequired()
          .HasConversion(c => Price.BASE_CURRENCY.ToString(), c => Price.BASE_CURRENCY);
      }
    );

    builder.OwnsOne(
      o => o.ShippingAddress,
      sb =>
      {
        sb.Property(sa => sa.Street).HasColumnName("ShippingStreet").IsRequired().HasMaxLength(255);
        sb.Property(sa => sa.City).HasColumnName("ShippingCity").IsRequired().HasMaxLength(100);
        sb.Property(sa => sa.State).HasColumnName("ShippingState").IsRequired().HasMaxLength(100);
        sb.Property(sa => sa.ZipCode)
          .HasColumnName("ShippingZipCode")
          .IsRequired()
          .HasMaxLength(10);
        sb.Property(sa => sa.Country)
          .HasColumnName("ShippingCountry")
          .IsRequired()
          .HasMaxLength(100);
      }
    );

    builder.OwnsOne(
      o => o.BillingAddress,
      sb =>
      {
        sb.Property(sa => sa.Street).HasColumnName("BillingStreet").IsRequired().HasMaxLength(255);
        sb.Property(sa => sa.City).HasColumnName("BillingCity").IsRequired().HasMaxLength(100);
        sb.Property(sa => sa.State).HasColumnName("BillingState").IsRequired().HasMaxLength(100);
        sb.Property(sa => sa.ZipCode).HasColumnName("BillingZipCode").IsRequired().HasMaxLength(10);
        sb.Property(sa => sa.Country)
          .HasColumnName("BillingCountry")
          .IsRequired()
          .HasMaxLength(100);
      }
    );

    builder
      .Property(o => o.Status)
      .HasColumnName("Status")
      .IsRequired()
      .HasConversion(s => s.ToString(), s => s.ConvertTo<OrderStatus>() ?? OrderStatus.Failed);

    builder.OwnsMany(
      o => o.OrderItems,
      oib =>
      {
        oib.ToTable("OrderItems");
        oib.WithOwner().HasForeignKey("OrderId");
        oib.HasKey(nameof(OrderItem.Id), "OrderId");

        oib.Property(oi => oi.Id)
          .ValueGeneratedNever()
          .HasColumnName("OrderItemId")
          .HasConversion(id => id.Value, value => OrderItemId.Convert(value))
          .IsRequired();

        oib.Property(oi => oi.Quantity).HasColumnName("Quantity").IsRequired();

        oib.Property(p => p.ProductId)
          .HasConversion(pi => pi.Value, v => ProductId.Convert(v))
          .HasColumnName("ProductId")
          .IsRequired();

        // relationship with Product entity
        oib.HasOne<Product>()
          .WithMany()
          .HasForeignKey("ProductId")
          .OnDelete(DeleteBehavior.Restrict);

        oib.OwnsOne(
          oi => oi.Price,
          pb =>
          {
            pb.Property(p => p.Value).HasColumnName("PriceValue").IsRequired();
            // pb.Property(p => p.Currency)
            //   .HasColumnName("PriceCurrency")
            //   .IsRequired()
            //   .HasConversion(c => c.ToString(), c => c.ConvertTo<Currency>() ?? Currency.None);
          }
        );
      }
    );

    builder.Navigation(o => o.OrderItems).UsePropertyAccessMode(PropertyAccessMode.Field);
  }
}
