using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Contracts.User;

public class WishlistProductRequest
{
    [Required]
    public string ProductId { get; set; } = null!;
}
