using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;

namespace Ecommerce.Domain.ProductAggregate.Entities;

public sealed class ProductReview : Entity<Guid>
{
    public UserId AuthorId { get; set; }
    public string Description { get; set; }
    public Rating Rating { get; set; }

    private ProductReview(Guid id, UserId authorId, string description, Rating rating)
      : base(id)
    {
        AuthorId = authorId;
        Description = description;
        Rating = rating;
    }

    public static ProductReview Create(UserId authorId, string description, Rating rating)
    {
        return new(Guid.NewGuid(), authorId, description, rating);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private ProductReview()
      : base(Guid.Empty) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}
