using Ecommerce.Domain.Common.Exceptions;

namespace Ecommerce.Domain.UserAggregate.Exceptions;

public class QuantityBelowZeroException : DomainException
{
  public QuantityBelowZeroException()
    : base("Product quantity should be more than 0.") { }
}
