namespace Ecommerce.Domain.UserAggregate.Exceptions;

using Ecommerce.Domain.Common.Exceptions;

public class InvalidCurrentPasswordException : DomainException
{
  public InvalidCurrentPasswordException()
    : base("The current password provided isn't correct.") { }
}
