using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Domain.NotificationAggregate;

namespace Ecommerce.Infrastructure.Persistence.EfCore.Repositories;

public class NotificationRepository(EfCoreContext context)
  : EfCoreRepository<Notification, Guid>(context),
    INotificationRepository { }
