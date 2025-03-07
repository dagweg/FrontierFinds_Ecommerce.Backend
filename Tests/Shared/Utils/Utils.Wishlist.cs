using Ecommerce.Application.UseCases.Users.Commands.WishlistProducts;

namespace Ecommerce.Tests.Shared;

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
