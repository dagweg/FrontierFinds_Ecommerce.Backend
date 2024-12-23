namespace Ecommerce.Domain.User;

using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.Common.ValueObjects;

public class User : AggregateRoot<UserId>
{
  public Name FirstName { get; private set; } = Name.Empty;
  public Name LastName { get; private set; } = Name.Empty;
  public Email Email { get; private set; } = Email.Empty;
  public Password Password { get; private set; } = Password.Empty;

  private User()
    : base(UserId.CreateUnique()) { }

  private User(UserId id, Name firstName, Name lastName, Email email, Password password)
    : base(id)
  {
    FirstName = firstName;
    LastName = lastName;
    Email = email;
    Password = password;
  }

  public static User Create(Name firstName, Name lastName, Email email, Password password) =>
    new(UserId.CreateUnique(), firstName, lastName, email, password);

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }
}
