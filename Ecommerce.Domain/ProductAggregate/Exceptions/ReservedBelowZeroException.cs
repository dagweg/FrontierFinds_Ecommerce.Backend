using Ecommerce.Domain.Common.Exceptions;

namespace Ecommerce.Domain.UserAggregate.Exceptions;

public class ReservedBelowZeroException : DomainException
{
  public ReservedBelowZeroException()
    : base("Reserved stock should be 0 or more.") { }
}
