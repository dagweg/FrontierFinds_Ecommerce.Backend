using Ecommerce.Domain.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ecommerce.Infrastructure.Persistence.EfCore.Interceptors;

public class PublishDomainEventsInterceptor : SaveChangesInterceptor
{
  private readonly IPublisher _publisher;

  public PublishDomainEventsInterceptor(IPublisher publisher)
  {
    _publisher = publisher;
  }

  public override InterceptionResult<int> SavingChanges(
    DbContextEventData eventData,
    InterceptionResult<int> result
  )
  {
    PublishDomainEvents(eventData.Context).GetAwaiter().GetResult();
    return base.SavingChanges(eventData, result);
  }

  public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default
  )
  {
    await PublishDomainEvents(eventData.Context);
    return await base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  public async Task PublishDomainEvents(DbContext? dbContext)
  {
    if (dbContext is null)
      return;

    var domainEventEntites = dbContext
      .ChangeTracker.Entries<IHasDomainEvent>()
      .Where(e => e.Entity.DomainEvents.Count > 0)
      .Select(e => e.Entity)
      .ToList();

    var domainEvents = domainEventEntites.SelectMany(e => e.DomainEvents).ToList();

    // Clear all the domain events to avoid recursive infinite publishing of events
    domainEventEntites.ForEach(e => e.ClearDomainEvents());

    // Publish to MediatR
    foreach (var domainEvent in domainEvents)
    {
      await _publisher.Publish(domainEvent);
    }
  }
}
