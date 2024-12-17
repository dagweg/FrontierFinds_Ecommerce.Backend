namespace Ecommerce.Domain.Common.ValueObjects;

using Ecommerce.Domain.Common.Models;

public class Password(string password) : ValueObject
{
    public string Value { get; set; } = password;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
