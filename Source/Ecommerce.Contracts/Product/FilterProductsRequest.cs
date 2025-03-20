namespace Ecommerce.Contracts.Product;

public record FilterProductsRequest(
  string? Name,
  long? MinPriceValueInCents,
  long? MaxPriceValueInCents,
  IEnumerable<int>? CategoryIds
);
