namespace Ecommerce.Domain.ProductAggregate.ValueObjects;

using Ecommerce.Domain.Common.Errors;
using Ecommerce.Domain.Common.Models;
using FluentResults;

public sealed class Stock : ValueObject
{
    public Quantity Quantity { get; set; } = Quantity.Empty;

    public static Stock Empty => new(Quantity.Empty);

    private Stock() { }

    private Stock(Quantity quantity)
    {
        Quantity = quantity;
    }

    public static Stock Create(Quantity quantity)
    {
        return new(quantity);
    }

    public void UpdateQuantity(Quantity quantity) => Quantity = quantity;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Quantity;
    }
}
