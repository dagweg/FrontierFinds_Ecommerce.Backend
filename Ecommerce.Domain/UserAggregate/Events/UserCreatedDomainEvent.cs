using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.UserAggregate.Events;

public record UserCreatedDomainEvent(User User) : IDomainEvent;
