using Ecommerce.Application.UseCases.Users.Commands.WishlistProducts;

namespace Ecommerce.UnitTests;

public partial class Utils
{
  public record Wishlist
  {
    public static WishlistProductsCommand CreateWishlistProductsCommand()
    {
      return new WishlistProductsCommand
      {
        ProductIds = new List<string> { Guid.NewGuid().ToString() },
      };
    }
  }
}
