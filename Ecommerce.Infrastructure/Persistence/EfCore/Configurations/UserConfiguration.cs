using Ecommerce.Application.Common.Extensions;
using Ecommerce.Domain.Common.Enums;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.NotificationAggregate;
using Ecommerce.Domain.NotificationAggregate.Enums;
using Ecommerce.Domain.OrderAggregate;
using Ecommerce.Domain.OrderAggregate.Entities;
using Ecommerce.Domain.OrderAggregate.Enums;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.Entities;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.EfCore.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasKey(u => u.Id);

    builder
      .Property(u => u.Id)
      .HasConversion(id => id.Value, value => UserId.Convert(value))
      .IsRequired();

    builder
      .Property(u => u.Email)
      .HasConversion(e => e.Value, v => Email.Create(v))
      .IsRequired()
      .HasMaxLength(255);

    builder
      .Property(u => u.FirstName)
      .HasConversion(f => f.Value, v => Name.Create(v))
      .IsRequired()
      .HasMaxLength(100);

    builder
      .Property(u => u.LastName)
      .HasConversion(l => l.Value, v => Name.Create(v))
      .IsRequired()
      .HasMaxLength(100);

    builder
      .Property(u => u.Password)
      .IsRequired()
      .HasConversion(p => p.Value, v => Password.Create(v))
      .HasMaxLength(255);

    builder
      .Property(u => u.PhoneNumber)
      .HasConversion(pn => pn.Value, v => PhoneNumber.Create(v))
      .IsRequired()
      .HasMaxLength(50);

    builder.Property(u => u.CountryCode).IsRequired().HasMaxLength(3).HasDefaultValue("251");

    builder.OwnsOne(
      u => u.Address,
      ob =>
      {
        ob.Property(a => a.City).IsRequired().HasMaxLength(100);
        ob.Property(a => a.State).IsRequired().HasMaxLength(100);
        ob.Property(a => a.Street).IsRequired().HasMaxLength(255);
        ob.Property(a => a.ZipCode).IsRequired().HasMaxLength(10);
      }
    );

    builder
      .OwnsMany(
        u => u.Cart,
        cb =>
        {
          cb.ToTable("UserCarts");
          cb.WithOwner().HasForeignKey("UserId");

          cb.HasKey(nameof(Cart.Id));
          cb.Property(c => c.Id)
            .HasColumnName("CartId")
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => CartId.Convert(value))
            .IsRequired();

          cb.OwnsMany(
            c => c.Items,
            cib =>
            {
              cib.ToTable("UserCartItems");
              cib.WithOwner().HasForeignKey("CartId");
              cib.HasKey(nameof(CartItem.Id), "CartId");
              cib.Property(ci => ci.Id)
                .HasColumnName("CartItemId")
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => CartItemId.Convert(value))
                .IsRequired();

              cib.Property(ci => ci.Quantity).HasColumnName("Quantity").IsRequired();

              cib.Property(p => p.ProductId)
                .HasConversion(pi => pi.Value, v => ProductId.Convert(v))
                .HasColumnName("ProductId")
                .IsRequired();

              // relationship with Product entity
              cib.HasOne<Product>()
                .WithMany()
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Restrict);
            }
          );

          cb.Navigation(c => c.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
      )
      .Metadata.FindNavigation(nameof(User.Cart))!
      .SetPropertyAccessMode(PropertyAccessMode.Field);

    builder
      .HasMany(u => u.Orders)
      .WithOne()
      .HasForeignKey(o => o.UserId)
      .OnDelete(DeleteBehavior.Cascade);

    builder
      .HasMany(u => u.Products)
      .WithOne()
      .HasForeignKey(p => p.SellerId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.OwnsOne(
      u => u.Wishlist,
      wb =>
      {
        wb.ToTable("Wishlists");
        wb.HasKey(nameof(Wishlist.Id));
        wb.WithOwner().HasForeignKey("UserId");
        wb.Property(w => w.Id).HasColumnName("WishlistId");
        wb.OwnsMany(
          w => w.ProductIds,
          wpb =>
          {
            wpb.ToTable("WishlistProducts");
            wpb.WithOwner().HasForeignKey("WishlistId");
            wpb.HasKey(nameof(ProductId.Value), "WishlistId");
            wpb.Property(pi => pi.Value).HasColumnName("ProductId").IsRequired();

            // relationship with Product entity
            wpb.HasOne<Product>().WithMany().OnDelete(DeleteBehavior.Restrict);
          }
        );
      }
    );

    // notification relationship is configured in NotificationConfigurations

    builder.ToTable("Users");
  }
}
