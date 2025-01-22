using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.ValueObjects;

namespace Ecommerce.Infrastructure.Persistence.EfCore.Repositories;

public class ProductRepository : EfCoreRepository<Product, ProductId>, IProductRepository
{
  public ProductRepository(EfCoreContext context)
    : base(context) { }
}
