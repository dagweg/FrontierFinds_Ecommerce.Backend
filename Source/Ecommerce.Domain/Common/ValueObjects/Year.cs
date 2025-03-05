using Ecommerce.Domain.Common.Errors;
using FluentResults;

public sealed record Year
{
  public int Value { get; }

  private Year() { }

  private const int MAX_YEARS_AHEAD = 50;

  private Year(int value)
  {
    Value = value;
  }

  public static Result<Year> Create(int year)
  {
    if (year < DateTime.Now.Year || year > DateTime.Now.Year + MAX_YEARS_AHEAD)
      return FormatError.GetResult(nameof(Year), "Year must be within a valid range.");

    return Result.Ok(new Year(year));
  }

  public override string ToString() => Value.ToString();
}
