using Ecommerce.Domain.Common.Enums;
using FluentResults;

namespace Ecommerce.Infrastructure.Common.Interfaces.Providers.Forex;

public interface IExchangeApiClient
{
  /// <summary>
  /// Get the exchange rate between two currencies
  /// </summary>
  /// <param name="fromCurrency"></param>
  /// <param name="toCurrency"></param>
  /// <returns></returns>
  Task<Result<decimal>> GetExchangeRateAsync(Currency fromCurrency, Currency toCurrency);
}
