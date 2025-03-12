using Ecommerce.Infrastructure.Services.Providers.Payment.Stripe;

namespace Ecommerce.Infrastructure.Services.Providers.Payment
{
  public class PaymentOptions
  {
    public const string SectionName = "PaymentSettings";
    public StripeOptions StripeOptions { get; set; } = new StripeOptions();
  }
}
