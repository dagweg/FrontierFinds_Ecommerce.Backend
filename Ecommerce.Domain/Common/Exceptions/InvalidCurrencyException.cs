using Ecommerce.Domain.Common.Enums;

namespace Ecommerce.Domain.Common.Exceptions;

public class InvalidCurrencyException : DomainException
{
  public InvalidCurrencyException(string currency)
    : base(
      $"Invalid currency: {currency}. Available currencies are {string.Join(", ", Enum.GetNames<Currency>().Skip(1))}"
    ) { }
}
