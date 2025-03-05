using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Domain.OrderAggregate;
using Ecommerce.Domain.OrderAggregate.ValueObjects;

namespace Ecommerce.Infrastructure.Persistence.EfCore.Repositories;

public class OrderRepository(EfCoreContext context)
  : EfCoreRepository<Order, OrderId>(context),
    IOrderRepository
{ }
