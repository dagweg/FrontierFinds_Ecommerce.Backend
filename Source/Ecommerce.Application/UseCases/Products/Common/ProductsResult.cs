using Ecommerce.Application.Common.Models.Enums;
using Ecommerce.Application.UseCases.Common.Interfaces;
using Ecommerce.Application.UseCases.Images.Common;
using Ecommerce.Domain.Common.Entities;

namespace Ecommerce.Application.UseCases.Products.Common;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
public class ProductsResult : ICollectionQueryResult
{
  public required decimal MinPriceValueInCents { get; set; }
  public required decimal MaxPriceValueInCents { get; set; }
  public required int TotalCount { get; set; }
  public required int TotalFetchedCount { get; set; }
  public IEnumerable<ProductResult> Products { get; set; }
  public DataSourceType_Debug DataSourceType { get; set; }
  public string DataSourceId { get; set; } = string.Empty;
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
