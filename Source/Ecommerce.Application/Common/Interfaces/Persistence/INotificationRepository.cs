using Ecommerce.Domain.NotificationAggregate;

namespace Ecommerce.Application.Common.Interfaces.Persistence;

public interface INotificationRepository : IRepository<Notification, Guid> { }
