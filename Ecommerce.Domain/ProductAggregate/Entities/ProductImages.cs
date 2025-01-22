using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.ProductAggregate.ValueObjects;

namespace Ecommerce.Domain.ProductAggregate.Entities;

public sealed class ProductImages : Entity<Guid>
{
  public string? LeftImageUrl { get; set; }
  public string? RightImageUrl { get; set; }
  public string? FrontImageUrl { get; set; }
  public string? BackImageUrl { get; set; }

  private ProductImages(Guid id)
    : base(id) { }

  public static ProductImages Create() => new(Guid.NewGuid());

  public ProductImages WithLeftImage(string leftImageUrl)
  {
    LeftImageUrl = leftImageUrl;
    return this;
  }

  public ProductImages WithRightImage(string rightImageUrl)
  {
    RightImageUrl = rightImageUrl;
    return this;
  }

  public ProductImages WithFrontImage(string frontImageUrl)
  {
    FrontImageUrl = frontImageUrl;
    return this;
  }

  public ProductImages WithBackImage(string backImageUrl)
  {
    BackImageUrl = backImageUrl;
    return this;
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  private ProductImages()
    : base(Guid.Empty) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}
