using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.ProductAggregate.ValueObjects;

namespace Ecommerce.Domain.ProductAggregate.Entities;

public sealed class ProductImages : Entity<Guid>
{
  public string? LeftImageUrl { get; private set; }
  public string? RightImageUrl { get; private set; }
  public string? FrontImageUrl { get; private set; }
  public string? BackImageUrl { get; private set; }

  private ProductImages(
    Guid id,
    string leftImageUrl,
    string rightImageUrl,
    string frontImageUrl,
    string backImageUrl
  )
    : base(id)
  {
    LeftImageUrl = leftImageUrl;
    RightImageUrl = rightImageUrl;
    FrontImageUrl = frontImageUrl;
    BackImageUrl = backImageUrl;
  }

  public static ProductImages Create(
    string leftImageUrl,
    string rightImageUrl,
    string frontImageUrl,
    string backImageUrl
  ) => new(Guid.NewGuid(), leftImageUrl, rightImageUrl, frontImageUrl, backImageUrl);

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  private ProductImages()
    : base(Guid.Empty) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}
