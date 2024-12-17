namespace Ecommerce.Domain.Common.ValueObjects;

using Ecommerce.Domain.Common.Models;

public class Name(string name) : ValueObject
{
    public string Value { get; set; } = name;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
