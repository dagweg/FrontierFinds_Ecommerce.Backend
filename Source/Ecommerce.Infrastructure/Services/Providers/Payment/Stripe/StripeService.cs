using Ecommerce.Application.Common.Interfaces.Providers.Payment.Stripe;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Domain.PaymentAggregate.Enums;
using Ecommerce.Infrastructure.Common;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace Ecommerce.Infrastructure.Services.Providers.Payment.Stripe;

public class StripeService(
  IOptions<ClientSettings> clientSettings,
  IOptions<PaymentOptions> paymentOptions
) : IStripeService
{
  public async Task<Result<CreateCheckoutSessionResult>> CreateCheckoutSessionAsync(
    CheckoutSessionRequest request
  )
  {
    var successUrl = clientSettings.Value.ClientBaseUrl + "/checkout/success";
    var cancelUrl = clientSettings.Value.ClientBaseUrl + "/checkout/cancel";

    StripeConfiguration.ApiKey = paymentOptions.Value.StripeSettings.SecretKey;

    var paymentMethodTypes = new HashSet<string> { "card" };

    // If there are other payment method types from the request -- add to the hashset

    var options = new SessionCreateOptions
    {
      PaymentMethodTypes = paymentMethodTypes.ToList(),
      LineItems = request
        .lineItems.Select(item => new SessionLineItemOptions
        {
          PriceData = new SessionLineItemPriceDataOptions
          {
            Currency = "usd", // TODO: get from request
            ProductData = new SessionLineItemPriceDataProductDataOptions
            {
              Name = item.productName,
              Description = $"{item.productDescription.Substring(0, 50)}...",
              Images = item.productImages.ToList(),
            },
            UnitAmount = item.amountInCents, // TODO: get from request (the currecy) and use exchange rate to evaluate the price
          },
          Quantity = item.quantity,
        })
        .ToList(),
      Mode = request.paymentMode == PaymentMode.Payment ? "payment" : "subscription",
      SuccessUrl = successUrl,
      CancelUrl = cancelUrl,
    };

    var service = new SessionService();
    var session = await service.CreateAsync(options);

    Console.WriteLine("Stripe Session");
    // LogPretty.Log(session);

    return new CreateCheckoutSessionResult(session.Id, session.Url);
  }
}
