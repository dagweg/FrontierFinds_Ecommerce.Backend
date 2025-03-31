using Ecommerce.Infrastructure.Services.Providers.Payment.Stripe;

namespace Ecommerce.Infrastructure.Services.Providers.Payment
{
  public class PaymentOptions
  {
    public const string SectionName = "PaymentSettings";
    public StripeOptions StripeSettings { get; set; } = null!;
  }
}
