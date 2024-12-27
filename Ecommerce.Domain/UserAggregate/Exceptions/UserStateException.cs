using Ecommerce.Domain.Common.Exceptions;

namespace Ecommerce.Domain.UserAggregate.Exceptions;

public class UserStateException : DomainException
{
  public UserStateException(string messagekey, Exception? ex = null)
    : base(messagekey, ex) { }
}
