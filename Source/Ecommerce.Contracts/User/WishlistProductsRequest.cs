using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Contracts.User;

public class WishlistProductsRequest
{
  [Required]
  [MinLength(1)]
  public List<WishlistProductRequest> ProductIds { get; set; } = null!;
}
