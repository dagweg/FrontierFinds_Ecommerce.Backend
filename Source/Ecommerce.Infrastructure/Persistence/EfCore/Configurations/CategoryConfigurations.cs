using Ecommerce.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.EfCore.Configurations;

public class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
  public void Configure(EntityTypeBuilder<Category> builder)
  {
    builder.HasKey(x => x.Id);

    builder.Property(x => x.Id);

    builder.Property(x => x.Name).IsRequired();
    builder.Property(x => x.Slug).IsRequired();
    builder
      .Property(x => x.IsActive)
      .HasDefaultValue(true)
      .HasConversion(x => x.ToString(), x => bool.Parse(x));

    builder
      .HasOne(x => x.Parent)
      .WithMany(x => x.SubCategories)
      .HasForeignKey(x => x.ParentId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}
