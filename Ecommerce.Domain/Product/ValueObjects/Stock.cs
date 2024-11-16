using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.Product.ValueObjects;

public sealed class Stock(int quantity, int reserved) : ValueObject
{
    public int Quantity { get; } = quantity;
    public int Reserved { get; } = reserved;


    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Quantity;
        yield return Reserved;

    }
}