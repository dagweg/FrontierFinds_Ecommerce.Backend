using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.EfCore.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder
      .Property(u => u.Id)
      .HasColumnName("Id")
      .HasConversion(e => e.Value, v => UserId.Convert(v))
      .IsRequired();

    builder.HasKey(u => u.Id);

    builder
      .OwnsOne(u => u.Email, ob => ob.Property(e => e.Value).HasColumnName("Email"))
      .OwnsOne(u => u.FirstName, ob => ob.Property(fn => fn.Value).HasColumnName("FirstName"))
      .OwnsOne(u => u.LastName, ob => ob.Property(ln => ln.Value).HasColumnName("LastName"))
      .OwnsOne(u => u.Password, ob => ob.Property(p => p.Value).HasColumnName("Password"))
      .OwnsOne(u => u.PhoneNumber, ob => ob.Property(p => p.Value).HasColumnName("PhoneNumber"));
  }
}
