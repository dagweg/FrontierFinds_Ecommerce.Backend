using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.ValueObjects;

namespace Ecommerce.Application.Common.Interfaces.Persistence;

public interface IProductRepository : IRepository<Product, ProductId> { }
