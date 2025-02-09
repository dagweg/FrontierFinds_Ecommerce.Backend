using Ecommerce.Application.UseCases.Images.Common;
using Ecommerce.Domain.Common.Entities;

namespace Ecommerce.Application.UseCases.Products.Common;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
public class ProductsResult
{
  public required int TotalCount { get; set; }
  public required int TotalFetchedCount { get; set; }
  public IEnumerable<ProductResult> Products { get; set; }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
