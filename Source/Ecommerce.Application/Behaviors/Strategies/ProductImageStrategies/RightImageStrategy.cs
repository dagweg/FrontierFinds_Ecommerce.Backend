using Ecommerce.Application.Common.Interfaces.Strategies;
using Ecommerce.Domain.ProductAggregate.Entities;

namespace Ecommerce.Application.Behaviors.Strategies.ProductImageStrategies;

public class RightImageStrategy : IProductImageStrategy
{
    public void Apply(ProductImages productImages, ProductImage productImage)
    {
        productImages.WithRightImage(productImage);
    }
}
