namespace Ecommerce.Domain.User;

using System.Runtime.InteropServices;
using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.Common.ValueObjects;

public class User : AggregateRoot<Guid>
{
    public Name FirstName { get; private set; } = Name.Empty;
    public Name LastName { get; private set; } = Name.Empty;
    public Email Email { get; private set; } = Email.Empty;
    public Password Password { get; private set; } = Password.Empty;

    private User()
        : base(Guid.NewGuid()) { }

    private User(Guid id, Name firstName, Name lastName, Email email, Password password)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public static User Create(Name firstName, Name lastName, Email email, Password password) =>
        new User(Guid.NewGuid(), firstName, lastName, email, password);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}
