using Ecommerce.Domain.Common.Exceptions;

namespace Ecommerce.Domain.UserAggregate.Exceptions;

public class ReservedBelowZeroException : DomainException
{
  public ReservedBelowZeroException(Exception? ex = null)
    : base("ReservedBelowZero", ex) { }
}
