using Ecommerce.Application.UnitTests.UseCases.Users.TestUtils;
using Ecommerce.Application.UseCases.Products.CreateUser.Commands;

namespace Ecommerce.Application.UnitTests.UseCases.Products.Commands.TestUtils;

public static class CreateProductTestUtils
{
  public static CreateProductCommand CreateCommand(int i = 0)
  {
    return new CreateProductCommand(
      ProductName: Constants.Product.ProductNameFromIndex(i),
      ProductDescription: Constants.Product.ProductDescriptionFromIndex(i),
      StockQuantity: Constants.Product.StockQuantity,
      PriceValue: Constants.Product.PriceValue,
      PriceCurrency: Constants.Product.PriceCurrency,
      Thumbnail: Constants.Product.Thumbnail.CreateCommand(i),
      LeftImage: Constants.Product.LeftImage.CreateCommand(i),
      RightImage: Constants.Product.RightImage.CreateCommand(i),
      FrontImage: Constants.Product.FrontImage.CreateCommand(i),
      BackImage: Constants.Product.BackImage.CreateCommand(i)
    );
  }
}
