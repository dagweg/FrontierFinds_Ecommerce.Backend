using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.ProductAggregate.ValueObjects;

namespace Ecommerce.Domain.ProductAggregate.Entities;

public sealed class ProductImages : Entity<Guid>
{
    public ProductImage Thumbnail { get; set; }
    public ProductImage? LeftImage { get; set; }
    public ProductImage? RightImage { get; set; }
    public ProductImage? FrontImage { get; set; }
    public ProductImage? BackImage { get; set; }
    public ProductImage? TopImage { get; set; }
    public ProductImage? BottomImage { get; set; }

    private ProductImages(Guid id, ProductImage thumbnail)
      : base(id)
    {
        Thumbnail = thumbnail;
    }

    public static ProductImages Create(ProductImage thumbnail) => new(Guid.NewGuid(), thumbnail);

    public ProductImages WithThumbnail(ProductImage thumbnail)
    {
        Thumbnail = thumbnail;
        return this;
    }

    public ProductImages WithLeftImage(ProductImage? leftImage)
    {
        LeftImage = leftImage;
        return this;
    }

    public ProductImages WithRightImage(ProductImage? rightImage)
    {
        RightImage = rightImage;
        return this;
    }

    public ProductImages WithFrontImage(ProductImage? frontImage)
    {
        FrontImage = frontImage;
        return this;
    }

    public ProductImages WithBackImage(ProductImage? backImage)
    {
        BackImage = backImage;
        return this;
    }

    public ProductImages WithTopImage(ProductImage? topImage)
    {
        TopImage = topImage;
        return this;
    }

    public ProductImages WithBottomImage(ProductImage? bottomImage)
    {
        BottomImage = bottomImage;
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
