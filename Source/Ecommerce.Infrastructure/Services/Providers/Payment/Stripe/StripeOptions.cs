namespace Ecommerce.Infrastructure.Services.Providers.Payment.Stripe;

public class StripeOptions
{
  public const string SectionName = "StripeSettings";

  public string SecretKey { get; set; } = null!;
  public string PublishableKey { get; set; } = null!;
}
