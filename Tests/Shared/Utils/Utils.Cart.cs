using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Users.Queries.GetCartItems;
using Ecommerce.Domain.ProductAggregate.ValueObjects;

namespace Ecommerce.Tests.Shared;

public partial class Utils
{
  public record Cart
  {
    public const int PageNumber = 1;
    public const int PageSize = 10;

    public static GetCartQuery CreateGetCartQuery()
    {
      return new GetCartQuery(PageNumber, PageSize);
    }

    public static CartResult CreateCartResult(int i = 1)
    {
      var cartItemResults = CartItem.CreateCartItemResults(i);
      var totalPrice = cartItemResults.Sum(x => x.Product.PriceValue * x.Quantity);
      return new CartResult
      {
        Items = CartItem.CreateCartItemResults(i),
        TotalItems = i,
        TotalPrice = totalPrice,
      };
    }

    public record CartItem
    {
      public const int Quantity = 10;
      public static readonly ProductId ProductId = ProductId.CreateUnique();

      public static Ecommerce.Domain.UserAggregate.Entities.CartItem Create()
      {
        return Ecommerce.Domain.UserAggregate.Entities.CartItem.Create(
          Utils.Cart.CartItem.ProductId,
          Utils.Cart.CartItem.Quantity
        );
      }

      public static List<Ecommerce.Domain.UserAggregate.Entities.CartItem> CreateCartItems(
        int i = 1
      )
      {
        return Enumerable.Range(1, i).Select(_ => Create()).ToList();
      }

      public static List<CartItemResult> CreateCartItemResults(int i = 1)
      {
        return Enumerable
          .Range(1, i)
          .Select(_ => new CartItemResult
          {
            Id = Utils.Cart.CartItem.ProductId.Value.ToString(),
            Quantity = Utils.Cart.CartItem.Quantity,
            Product = Utils.Product.CreateProductResult(Utils.Product.CreateProduct()),
          })
          .ToList();
      }
    }
  }
}
