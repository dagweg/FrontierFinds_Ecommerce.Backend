namespace Ecommerce.Domain.UserAggregate;

using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate.Events;
using Ecommerce.Domain.UserAggregate.Exceptions;
using Ecommerce.Domain.UserAggregate.ValueObjects;

public class User : AggregateRoot<UserId>
{
  public Name FirstName { get; private set; } = Name.Empty;
  public Name LastName { get; private set; } = Name.Empty;
  public Email Email { get; private set; } = Email.Empty;
  public Password Password { get; private set; } = Password.Empty;
  public PhoneNumber PhoneNumber { get; private set; } = PhoneNumber.Empty;

  private User()
    : base(UserId.CreateUnique()) { }

  private User(
    UserId id,
    Name firstName,
    Name lastName,
    Email email,
    Password password,
    PhoneNumber phoneNumber
  )
    : base(id)
  {
    FirstName = firstName;
    LastName = lastName;
    Email = email;
    Password = password;
    PhoneNumber = phoneNumber;
  }

  public static User Create(
    Name firstName,
    Name lastName,
    Email email,
    Password password,
    PhoneNumber phoneNumber
  )
  {
    var user = new User(UserId.CreateUnique(), firstName, lastName, email, password, phoneNumber);

    user.AddDomainEvent(new UserCreatedDomainEvent(user));

    return user;
  }

  public void ChangePassword(Password newPassword, Password currentPassword)
  {
    if (!Password.Equals(currentPassword))
      throw new InvalidCurrentPasswordException();

    Password = newPassword;
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }
}
