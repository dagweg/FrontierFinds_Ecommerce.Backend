namespace Ecommerce.Domain.UserAggregate.ValueObjects;

using Ecommerce.Domain.Common.Models;

public sealed class Name : ValueObject
{
    public string Value { get; set; }

    public static Name Empty => new(string.Empty);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Name() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private Name(string name)
    {
        Value = name;
    }

    public static Name Create(string name) => new(name);

    public static implicit operator string(Name name) => name.Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
