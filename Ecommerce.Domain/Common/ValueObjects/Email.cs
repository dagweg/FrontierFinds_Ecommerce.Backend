namespace Ecommerce.Domain.Common.ValueObjects;

using Ecommerce.Domain.Common.Models;

public class Email(string email) : ValueObject
{
    public string Value { get; set; } = email;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
