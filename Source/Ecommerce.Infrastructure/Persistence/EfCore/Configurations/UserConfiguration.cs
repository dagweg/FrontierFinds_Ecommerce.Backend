using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Domain.Common.Entities;
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

#pragma warning disable CS0618 // Type or member is obsolete
    builder
      .Property(u => u.Password)
      .IsRequired()
      .HasConversion(p => p.ValueHash, v => Password.Create(v))
      .HasMaxLength(255);
#pragma warning restore CS0618 // Type or member is obsolete

    builder
      .Property(u => u.PhoneNumber)
      .HasConversion(pn => pn.Value, v => PhoneNumber.Create(v))
      .IsRequired()
      .HasMaxLength(50);

    builder.OwnsOne(
      u => u.ProfileImage,
      b =>
      {
        b.HasIndex(x => x.ObjectIdentifier).IsUnique();
        b.Property(x => x.Url);
        b.Property(x => x.ObjectIdentifier);
      }
    );

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

    builder.OwnsOne(
      u => u.BillingAddress,
      b =>
      {
        b.Property(x => x.Country).HasColumnName("BillingAddress_Country");
        b.Property(x => x.State).HasColumnName("BillingAddress_State");
        b.Property(x => x.City).HasColumnName("BillingAddress_City");
        b.Property(x => x.Street).HasColumnName("BillingAddress_Street");
        b.Property(x => x.ZipCode).HasColumnName("BillingAddress_ZipCode");
      }
    );

    builder.OwnsOne(
      u => u.EmailVerificationOtp,
      ob =>
      {
        ob.Property(a => a.Value)
          .HasConversion(
            v => v == null ? null : string.Join("", v),
            s => s == null ? null : ConversionUtility.ToIntArray(s).Value
          )
          .IsRequired();
        ob.Property(a => a.Expiry).IsRequired();
        ob.Property(a => a.NextResendValidAt)
          .HasColumnName("EmailVerificationOtpNextResendValidAt");
        ob.Property(a => a.ResendFailStreak);
      }
    );

    builder.OwnsOne(
      u => u.PasswordResetOtp,
      ob =>
      {
        ob.Property(a => a.Value)
          .HasConversion(
            v => v == null ? null : string.Join("", v),
            s => s == null ? null : ConversionUtility.ToIntArray(s).Value
          )
          .IsRequired();
        ob.Property(a => a.Expiry).IsRequired();
        ob.Property(a => a.NextResendValidAt).HasColumnName("PasswordResetOtpNextResendValidAt");
        ob.Property(a => a.ResendFailStreak);
      }
    );

    builder
      .OwnsOne(
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
