using Ecommerce.Domain.UserAggregate;
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
      .HasColumnName("Id")
      .HasConversion(id => id.Value, value => UserId.Convert(value))
      .IsRequired();

    builder.OwnsOne(
      u => u.Email,
      ob =>
      {
        ob.Property(e => e.Value).HasColumnName("Email").IsRequired().HasMaxLength(255);
      }
    );

    builder.OwnsOne(
      u => u.FirstName,
      ob =>
      {
        ob.Property(fn => fn.Value).HasColumnName("FirstName").IsRequired().HasMaxLength(100);
      }
    );

    builder.OwnsOne(
      u => u.LastName,
      ob =>
      {
        ob.Property(ln => ln.Value).HasColumnName("LastName").IsRequired().HasMaxLength(100);
      }
    );

    builder.OwnsOne(
      u => u.Password,
      ob =>
      {
        ob.Property(p => p.Value).HasColumnName("Password").IsRequired().HasMaxLength(512);
      }
    );

    builder.OwnsOne(
      u => u.PhoneNumber,
      ob =>
      {
        ob.Property(p => p.Value).HasColumnName("PhoneNumber").IsRequired().HasMaxLength(15);
      }
    );

    builder.ToTable("Users");
  }
}
