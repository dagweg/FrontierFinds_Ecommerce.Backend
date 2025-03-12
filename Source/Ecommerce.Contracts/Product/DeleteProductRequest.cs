using System.ComponentModel.DataAnnotations;
using Ecommerce.Api.Attributes;

namespace Ecommerce.Contracts.Product;

public class DeleteProductRequest
{
  [Required]
  [MinLength(1)]
  [GuidList]
  public List<string> ProductIds { get; init; } = [];
}
