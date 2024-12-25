using Ecommerce.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.EfCore.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.OwnsOne(
      u => u.Email,
      ob =>
      {
        ob.Property(e => e.Value).HasColumnName("Email");
      }
    );
  }
}
