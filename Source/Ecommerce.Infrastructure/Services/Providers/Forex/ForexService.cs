using Ecommerce.Application.Common.Interfaces.Providers.Forex;
using Ecommerce.Domain.Common.Enums;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Infrastructure.Common.Interfaces.Providers.Forex;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Services.Providers.Forex;

public class ForexService(IExchangeApiClient exchangeApiClient, ILogger<ForexService> logger)
  : IForexSerivce
{
  public async Task<Result<long>> ConvertToBaseCurrencyAsync(
    long amountInCents,
    Currency fromCurrency
  )
  {
    var rateResult = await exchangeApiClient.GetExchangeRateAsync(
      fromCurrency,
      Price.BASE_CURRENCY
    );

    if (rateResult.IsFailed)
      return rateResult.ToResult();

    logger.LogInformation(
      "Converted {AmountInCents} {FromCurrency} to {BaseCurrency} with rate {Rate}",
      amountInCents,
      fromCurrency,
      Price.BASE_CURRENCY,
      rateResult.Value
    );
    return (long)Math.Round(rateResult.Value * amountInCents);
  }
}
