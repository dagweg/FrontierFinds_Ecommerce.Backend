using Ecommerce.Domain.Common.Exceptions;

namespace Ecommerce.Domain.UserAggregate.Exceptions;

public class QuantityBelowZeroException : DomainException
{
  public QuantityBelowZeroException(Exception? ex = null)
    : base("QuantityBelowZero", ex) { }
}
