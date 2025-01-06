using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.NotificationAggregate.Enums;
using Ecommerce.Domain.UserAggregate.ValueObjects;

namespace Ecommerce.Domain.NotificationAggregate;

public class Notification : AggregateRoot<Guid>
{
  public UserId UserId { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public NotificationType NotificationType { get; set; }
  public NotificationStatus NotificationStatus { get; set; }
  public DateTime? ReadAt { get; } = null;

  private List<NotificationAction> _actions = [];

  public IReadOnlyCollection<NotificationAction> Actions => _actions.AsReadOnly();

  private Notification(
    Guid id,
    UserId userId,
    NotificationType notificationType,
    string title,
    string description,
    NotificationStatus notificationStatus,
    DateTime? readAt,
    List<NotificationAction> actions
  )
    : base(id)
  {
    UserId = userId;
    NotificationType = notificationType;
    Title = title;
    Description = description;
    NotificationStatus = notificationStatus;
    ReadAt = readAt;
    _actions = actions;
  }

  public static Notification Create(
    Guid id,
    UserId userId,
    NotificationType notificationType,
    string title,
    string description,
    NotificationStatus notificationStatus,
    DateTime? readAt,
    List<NotificationAction> actions
  )
  {
    return new(
      id,
      userId,
      notificationType,
      title,
      description,
      notificationStatus,
      readAt,
      actions
    );
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  private Notification()
    : base(Guid.Empty) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}
