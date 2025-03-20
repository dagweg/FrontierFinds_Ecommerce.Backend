namespace Ecommerce.Application.UseCases.Products.Common;

public class CategoryResult
{
  public required int Id { get; set; }
  public required string Name { get; set; }
  public required string Slug { get; set; }
  public required bool IsActive { get; set; } = true;
  public required int? ParentId { get; set; }
  public List<CategoryResult> SubCategories { get; set; } = [];
}
