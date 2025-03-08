using Ecommerce.Application.Common.Extensions;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.PaymentAggregate;
using Ecommerce.Domain.PaymentAggregate.Enums;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.EfCore.Configurations;

internal sealed class PaymentConfigurations : IEntityTypeConfiguration<Payment>
{
  public void Configure(EntityTypeBuilder<Payment> builder)
  {
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id).ValueGeneratedNever();
    builder
      .Property(x => x.Status)
      .IsRequired()
      .HasConversion(s => s.ToString(), s => s.ConvertTo<PaymentStatus>() ?? PaymentStatus.Failed);

    builder.OwnsOne(
      p => p.Price,
      pb =>
      {
        pb.Property(p => p.Value).HasColumnName("PriceValue").IsRequired();
      }
    );

    builder
      .Property(x => x.PaymentMethod)
      .IsRequired()
      .HasConversion(m => m.ToString(), m => m.ConvertTo<PaymentMethod>() ?? PaymentMethod.Invalid);

    builder
      .Property(x => x.OrderItemId)
      .IsRequired()
      .HasConversion(o => o.Value, o => OrderItemId.Convert(o));

    builder
      .Property(x => x.PayerId)
      .IsRequired()
      .HasConversion(p => p.Value, p => UserId.Convert(p));

    builder.Property(x => x.TransactionId).IsRequired();
    builder.Property(x => x.FailureReason);
    builder.Property(x => x.PaidAt);

    builder.ToTable("Payments");
  }
}
