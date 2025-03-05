using Ecommerce.Application.Common.Interfaces.Strategies;
using Ecommerce.Domain.ProductAggregate.Enums;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.Behaviors.Strategies.ProductImageStrategies;

public interface IProductImageStrategyResolver
{
    IProductImageStrategy Resolve(ProductView productView);
}

public class ProductImageStrategyResolver : IProductImageStrategyResolver
{
    private readonly Dictionary<ProductView, IProductImageStrategy> _strategies;

    public ProductImageStrategyResolver()
    {
        _strategies = new Dictionary<ProductView, IProductImageStrategy>
    {
      { ProductView.Thumbnail, new ThumbnailStrategy() },
      { ProductView.Front, new FrontImageStrategy() },
      { ProductView.Back, new BackImageStrategy() },
      { ProductView.Left, new LeftImageStrategy() },
      { ProductView.Right, new RightImageStrategy() },
      { ProductView.Top, new TopImageStrategy() },
      { ProductView.Bottom, new BottomImageStrategy() },
    };
    }

    /// <summary>
    ///  Resolve the strategy for the given product view.
    /// </summary>
    /// <param name="productView"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public IProductImageStrategy Resolve(ProductView productView)
    {
        if (_strategies.TryGetValue(productView, out var strategy))
            return strategy;

        throw new InvalidOperationException($"No strategy found for {productView}");
    }
}
