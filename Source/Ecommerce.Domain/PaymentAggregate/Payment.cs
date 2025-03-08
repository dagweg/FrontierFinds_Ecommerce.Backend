using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.OrderAggregate.Enums;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.PaymentAggregate.Enums;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;

namespace Ecommerce.Domain.PaymentAggregate;

public class Payment : AggregateRoot<Guid>
{
  public PaymentStatus Status { get; private set; } = PaymentStatus.Pending;
  public Price Price { get; private set; }
  public PaymentMethod PaymentMethod { get; private set; }

  public OrderItemId OrderItemId { get; private set; }
  public UserId PayerId { get; private set; }

  public string TransactionId { get; private set; }
  public string? FailureReason { get; private set; }

  public DateTime? PaidAt { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  private Payment()
    : base(Guid.NewGuid()) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

  private Payment(
    Guid id,
    Price price,
    PaymentMethod paymentMethod,
    OrderItemId orderItemId,
    UserId payerId,
    string transactionId
  )
    : base(id)
  {
    Price = price;
    PaymentMethod = paymentMethod;
    OrderItemId = orderItemId;
    PayerId = payerId;
    TransactionId = transactionId;
  }

  public static Result<Payment> Create(
    Price price,
    PaymentMethod paymentMethod,
    OrderItemId orderItemId,
    UserId payerId
  )
  {
    var transactionId = "txn_" + Guid.NewGuid().ToString();
    return Result.Ok(
      new Payment(Guid.NewGuid(), price, paymentMethod, orderItemId, payerId, transactionId)
    );
  }

  public void MarkAsPaid()
  {
    Status = PaymentStatus.Success;
    PaidAt = DateTime.UtcNow;
  }

  public void MarkAsFailed(string reason)
  {
    Status = PaymentStatus.Failed;
    FailureReason = reason;
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }
}
