using Ecommerce.Application.Common.Interfaces.Processors;
using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.ProductAggregate.Entities;
using FluentValidation;

namespace Ecommerce.Application.UseCases.Images.CreateImage;

public class CreateImageCommandValidator : AbstractValidator<CreateImageCommand>
{
  private readonly IImageProcessor _imageProcessor;

  public CreateImageCommandValidator(IImageProcessor imageProcessor, bool isRequired = true)
  {
    _imageProcessor = imageProcessor;

    When(
      x => isRequired,
      () =>
      {
        RuleFor(i => i.ImageStream)
          .NotNull()
          .WithMessage("Image stream cannot be null when required.")
          .Must(i => i.CanRead)
          .WithMessage("Image stream must be readable.")
          .Must(i => i.Length > 0)
          .WithMessage("Image stream cannot be empty.")
          .Must(i => i.Length <= Image.SIZE_LIMIT_BYTES)
          .WithMessage(
            $"Image size must be less than {Image.SIZE_LIMIT_BYTES / (1024 * 1024)} MB."
          );

        // Ensure the stream is at the start or can be reset
        RuleFor(i => i.ImageStream)
          .Must(i =>
            !i.CanSeek || i.Position == 0 || (i.Position > 0 && i.Seek(0, SeekOrigin.Begin) == 0)
          )
          .WithMessage("Image stream must be at the start or seekable to the start.");

        // Validate image format
        RuleFor(i => i.ImageStream)
          .Must(BeSupportedImageFormat)
          .WithMessage(i =>
            $"Unsupported image format: {GetDetectedFormat(i.ImageStream) ?? "unknown"}"
          );
      }
    );

    When(
      x => !isRequired,
      () =>
      {
        RuleFor(i => i.ImageStream)
          .Must(i =>
            i == null
            || (
              i.CanRead
              && i.Length > 0
              && i.Length <= Image.SIZE_LIMIT_BYTES
              && BeSupportedImageFormat(i)
            )
          )
          .WithMessage(i =>
            i.ImageStream == null
              ? "Image stream is optional."
              : $"Image stream, if provided, must be readable, non-empty, less than {Image.SIZE_LIMIT_BYTES / (1024 * 1024)} MB, and in a supported format."
          );
      }
    );
  }

  private bool BeSupportedImageFormat(Stream? stream)
  {
    if (stream == null)
      return true; // Handled by other rules when required
    var formatResult = _imageProcessor.GetImageFormat(stream);
    if (formatResult.IsFailed || string.IsNullOrEmpty(formatResult.Value))
    {
      return false;
    }
    return _imageProcessor.IsFormatSupported(formatResult.Value).Value;
  }

  private string? GetDetectedFormat(Stream? stream)
  {
    if (stream == null)
      return null;
    var formatResult = _imageProcessor.GetImageFormat(stream);
    return formatResult.IsSuccess ? formatResult.Value : null;
  }

  public static CreateImageCommandValidator GetValidator(
    IImageProcessor imageProcessor,
    bool isRequired = true
  ) => new CreateImageCommandValidator(imageProcessor, isRequired);
}
