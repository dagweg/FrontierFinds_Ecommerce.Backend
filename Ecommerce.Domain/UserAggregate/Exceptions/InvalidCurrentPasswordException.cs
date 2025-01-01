namespace Ecommerce.Domain.UserAggregate.Exceptions;

using Ecommerce.Domain.Common.Exceptions;

public class InvalidCurrentPasswordException : DomainException
{
  public InvalidCurrentPasswordException(Exception? ex = null)
    : base("InvalidCurrentPassword", ex) { }
}
