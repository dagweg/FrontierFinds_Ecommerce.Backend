using MediatR;

namespace Ecommerce.Domain.Common.Models;

/// <summary>
/// Marker interface for Domain Events
/// </summary>
public interface IDomainEvent : INotification { }
