using System;
using System.Threading.Tasks;
using Ecommerce.Application.Common.Interfaces.Providers.Payment.Stripe;
using Ecommerce.Domain.PaymentAggregate.Enums;
using Ecommerce.Infrastructure.Common;
using Ecommerce.Infrastructure.Services.Providers.Payment;
using Ecommerce.Infrastructure.Services.Providers.Payment.Stripe;
using Ecommerce.IntegrationTests;
using Ecommerce.Tests.Shared;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Ecommerce_Platform.NET.Tests.IntegrationTests.Infrastructure.Services.Providers.Payment
{
  public class StripeServiceTests : IntegrationTestBase
  {
    private readonly IStripeService _stripeService;

    // private readonly Mock<IOptions<ClientSettings>> _clientSettingsMock;
    // private readonly Mock<IOptions<PaymentOptions>> _paymentOptionsMock;

    private readonly ClientSettings _clientSettings;
    private readonly PaymentOptions _paymentOptions;

    public StripeServiceTests(
      IOptions<ClientSettings> clientSettings,
      IOptions<PaymentOptions> paymentOptions,
      EcommerceWebApplicationFactoryFixture fixture
    )
      : base(fixture)
    {
      // _clientSettingsMock = new Mock<IOptions<ClientSettings>>();
      // _paymentOptionsMock = new Mock<IOptions<PaymentOptions>>();

      // _clientSettingsMock.Setup(x => clientSettings);

      // _paymentOptionsMock.Setup(x => paymentOptions);

      _clientSettings = clientSettings.Value;
      _paymentOptions = paymentOptions.Value;
      _stripeService = new StripeService(clientSettings, paymentOptions);
    }

    [Fact]
    public async Task CreateCheckoutSessionShould_ReturnOk_WhenCheckoutSessionRequestIsCorrect()
    {
      // Arrange
      var request = new CheckoutSessionRequest
      {
        lineItems = [new OrderItemCheckout("line-item-1", 1, 10000)],
        paymentMethod = PaymentMethod.CreditCard,
        paymentMode = PaymentMode.Payment,
      };

      // Act
      var result = await _stripeService.CreateCheckoutSessionAsync(request);

      // Assert
      result.IsSuccess.Should().BeTrue();
      result
        .Value.redirectUrl.Should()
        .Be(_clientSettings.ClientBaseUrl + "/payment/checkout/success");
      result.Value.sessionId.Should().NotBeNull();
    }
  }
}
