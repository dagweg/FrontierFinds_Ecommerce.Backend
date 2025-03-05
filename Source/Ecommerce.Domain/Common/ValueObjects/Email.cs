namespace Ecommerce.Domain.Common.ValueObjects;

using Ecommerce.Domain.Common.Models;

public sealed class Email : ValueObject
{
    public string Value { get; set; }

    public static Email Empty => new(string.Empty);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Email() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private Email(string email)
    {
        Value = email;
    }

    public static Email Create(string email) => new(email);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Email email) => email.Value;
}
