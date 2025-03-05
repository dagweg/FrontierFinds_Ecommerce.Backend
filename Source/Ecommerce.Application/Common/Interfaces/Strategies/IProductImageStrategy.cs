using Ecommerce.Domain.ProductAggregate.Entities;

namespace Ecommerce.Application.Common.Interfaces.Strategies;

public interface IProductImageStrategy
{
    void Apply(ProductImages productImages, ProductImage productImage);
}
