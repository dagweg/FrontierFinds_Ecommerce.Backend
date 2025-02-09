namespace Ecommerce.Domain.ProductAggregate.ValueObjects;

using Ecommerce.Domain.Common.Errors;
using Ecommerce.Domain.Common.Models;
using FluentResults;

public sealed class Stock : ValueObject
{
  public Quantity Quantity { get; set; } = Quantity.Empty;
  public int Reserved { get; set; } = 0;

  public static Stock Empty => new(Quantity.Empty, 0);

  private Stock() { }

  private Stock(Quantity quantity, int reserved)
  {
    Quantity = quantity;
    Reserved = reserved;
  }

  public static Stock Create(Quantity quantity, int reserved = 0)
  {
    return new(quantity, reserved);
  }

  public void UpdateQuantity(Quantity quantity) => Quantity = quantity;

  public Result<Stock> UpdateReserved(int reserved)
  {
    if (reserved < 0)
      return BelowZeroError.GetResult(nameof(reserved), "Cannot be less than 0.");
    Reserved = reserved;

    return Result.Ok(this);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Quantity;
    yield return Reserved;
  }
}
