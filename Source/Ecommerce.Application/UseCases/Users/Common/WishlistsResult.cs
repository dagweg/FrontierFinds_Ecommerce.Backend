using Ecommerce.Application.UseCases.Products.Common;

namespace Ecommerce.Application.UseCases.Users.Common;

public class WishlistsResult
{
    public required int TotalItems { get; set; }
    public IEnumerable<ProductResult> Wishlists { get; set; } = [];
}
