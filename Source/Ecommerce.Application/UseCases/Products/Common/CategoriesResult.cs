namespace Ecommerce.Application.UseCases.Products.Common;

public class CategoriesResult
{
  public required IEnumerable<CategoryResult> Categories { get; set; } = [];
}
