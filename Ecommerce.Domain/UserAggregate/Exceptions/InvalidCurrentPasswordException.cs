using Ecommerce.Domain.Common.Exceptions;

namespace Ecommerce.Domain.UserAggregate.Exceptions;

public class InvalidCurrentPasswordException : DomainException
{
  public InvalidCurrentPasswordException(Exception? ex = null)
    : base("InvalidCurrentPassword", ex) { }
}
