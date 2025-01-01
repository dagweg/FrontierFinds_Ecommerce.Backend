using Ecommerce.Application.Common.Extensions;
using Ecommerce.Domain.NotificationAggregate;
using Ecommerce.Domain.NotificationAggregate.Enums;
using Ecommerce.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.EfCore.Configurations;

public class NotificationConfigurations : IEntityTypeConfiguration<Notification>
{
  public void Configure(EntityTypeBuilder<Notification> builder)
  {
    builder.ToTable("Notifications");

    builder.HasKey(nameof(Notification.Id), "UserId");

    builder.Property(n => n.Id).ValueGeneratedNever().HasColumnName("NotificationId").IsRequired();

    // relationship with User
    builder.Property(n => n.UserId).HasColumnName("UserId").IsRequired();
    builder
      .HasOne<User>()
      .WithMany(u => u.Notifications)
      .HasForeignKey(n => n.UserId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.Property(n => n.Title).HasColumnName("Title").IsRequired().HasMaxLength(255);
    builder
      .Property(n => n.Description)
      .HasColumnName("Description")
      .IsRequired()
      .HasMaxLength(1000);

    builder
      .Property(n => n.NotificationType)
      .HasConversion(
        t => t.ToString(),
        t => t.ConvertTo<NotificationType>() ?? NotificationType.None
      )
      .HasColumnName("NotificationType")
      .IsRequired();
    builder
      .Property(n => n.NotificationStatus)
      .HasConversion(
        s => s.ToString(),
        s => s.ConvertTo<NotificationStatus>() ?? NotificationStatus.None
      )
      .HasColumnName("NotificationStatus")
      .IsRequired();

    builder.Property(n => n.ReadAt).HasColumnName("ReadAt").IsRequired();
  }
}
