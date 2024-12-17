namespace Ecommerce.Domain.User;

using System.Runtime.InteropServices;
using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.Common.ValueObjects;

public class User : AggregateRoot<Guid>
{
    public Name FirstName { get; }
    public Name LastName { get; }
    public Email Email { get; }
    public Password Password { get; }

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
