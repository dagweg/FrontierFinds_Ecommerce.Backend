using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.OrderAggregate.Events;

public record OrderCreatedDomainEvent(Order Order) : IDomainEvent;
