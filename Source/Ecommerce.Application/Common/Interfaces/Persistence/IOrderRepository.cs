using Ecommerce.Domain.OrderAggregate;
using Ecommerce.Domain.OrderAggregate.ValueObjects;

namespace Ecommerce.Application.Common.Interfaces.Persistence;

public interface IOrderRepository : IRepository<Order, OrderId> { }
