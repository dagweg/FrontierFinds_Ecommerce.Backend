using Ecommerce.Application.Common.Interfaces.Providers.Forex;
using Ecommerce.Domain.Common.Enums;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Infrastructure.Common.Interfaces.Providers.Forex;
using FluentResults;

namespace Ecommerce.Infrastructure.Services.Providers.Forex;

public class ForexService(IExchangeApiClient exchangeApiClient) : IForexSerivce
{
  public async Task<Result<decimal>> ConvertToBaseCurrencyAsync(
    decimal amount,
    Currency fromCurrency
  )
  {
    var rateResult = await exchangeApiClient.GetExchangeRateAsync(
      fromCurrency,
      Price.BASE_CURRENCY
    );

    if (rateResult.IsFailed)
      return rateResult;

    return rateResult.Value * amount;
  }
}
