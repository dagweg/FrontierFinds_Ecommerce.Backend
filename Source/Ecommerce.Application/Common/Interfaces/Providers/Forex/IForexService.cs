using Ecommerce.Domain.Common.Enums;
using FluentResults;

namespace Ecommerce.Application.Common.Interfaces.Providers.Forex;

public interface IForexSerivce
{
  /// <summary>
  /// Converts the given amount to the base currency of the application
  ///  - The base currency is the currency that the application uses for all its calculations
  ///  - This should be used before saving the amount to the database (since the db stores the amount in the base currency)
  /// </summary>
  /// <returns></returns>
  Task<Result<long>> ConvertToBaseCurrencyAsync(long amountInCents, Currency fromCurrency);
}
