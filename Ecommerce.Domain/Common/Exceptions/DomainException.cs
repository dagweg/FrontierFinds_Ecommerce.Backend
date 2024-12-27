namespace Ecommerce.Domain.Common.Exceptions;

/// <summary>
/// This is the base exception that will be
/// thrown from the domain layer to be handled
/// appropriately inside our application layer.
/// The <code>MessageKey</code> should be correctly
/// used in the upper layers for mapping to messages
/// found in resource or other files.
/// </summary>
public abstract class DomainException : Exception
{
  public string MessageKey { get; }

  public DomainException(string messagekey, Exception? ex = null)
    : base(messagekey, ex)
  {
    MessageKey = messagekey;
  }
}
