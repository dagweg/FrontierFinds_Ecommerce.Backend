using MediatR;

namespace Ecommerce.Application.Services.Workers.Cloudinary;

public record CloudinaryTaskNotification : INotification
{
  public required List<string> ObjectIds { get; init; }
  public required CloudinaryAction CloudinaryAction { get; init; }
}
