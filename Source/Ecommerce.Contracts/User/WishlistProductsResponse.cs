using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Contracts.User;

public class WishlistProductsResponse
{
  [Required]
  public string ProductId { get; set; } = null!;
}
