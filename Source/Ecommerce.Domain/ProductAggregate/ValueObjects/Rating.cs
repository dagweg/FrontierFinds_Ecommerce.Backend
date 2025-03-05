using Ecommerce.Domain.Common.Errors;
using Ecommerce.Domain.Common.Models;
using FluentResults;

namespace Ecommerce.Domain.ProductAggregate.ValueObjects;

public sealed class Rating : ValueObject
{
    public decimal Value { get; set; }

    public static Rating Empty => new(0);

    private const decimal MIN_RATING = 0;
    private const decimal MAX_RATING = 5;

    private Rating() { }

    private Rating(decimal value)
    {
        Value = value;
    }

    public static Result<Rating> Create(decimal value)
    {
        if (value < MIN_RATING || value > MAX_RATING)
            return OutOfBoundsError<decimal>.GetResult(
              nameof(value),
              "Out of bounds.",
              MIN_RATING,
              MAX_RATING
            );
        return new Rating(value);
    }

    public static Rating CreateEmpty() => new(0);

    public static implicit operator decimal(Rating rating) => rating.Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
