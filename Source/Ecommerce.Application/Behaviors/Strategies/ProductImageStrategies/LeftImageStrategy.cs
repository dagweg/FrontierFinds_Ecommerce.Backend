using Ecommerce.Application.Common.Interfaces.Strategies;
using Ecommerce.Domain.ProductAggregate.Entities;

namespace Ecommerce.Application.Behaviors.Strategies.ProductImageStrategies;

public class LeftImageStrategy : IProductImageStrategy
{
    public void Apply(ProductImages productImages, ProductImage productImage)
    {
        productImages.WithLeftImage(productImage);
    }
}
