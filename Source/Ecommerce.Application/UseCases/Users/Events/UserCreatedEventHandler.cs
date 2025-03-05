using Ecommerce.Domain.UserAggregate.Events;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Events;

public class UserCreatedEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
  public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
  {
    Console.WriteLine("We just handled our first domain event. IT IS USER CREATED DOMAIN EVENT");
    return Task.CompletedTask;
  }
}
