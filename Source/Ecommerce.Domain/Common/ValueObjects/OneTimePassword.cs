using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common.Errors;
using Ecommerce.Domain.Common.Models;
using FluentResults;

namespace Ecommerce.Domain.Common.ValueObjects;

public class OneTimePassword : ValueObject
{
  #region Constants

  [NotMapped]
  public const int OTP_LENGTH = 6;

  [NotMapped]
  public const int EXPIRY_MINUTES = 10;

  #endregion

  #region Properties

  public int[] Value { get; private set; }
  public DateTime Expiry { get; private set; }

  #endregion


  #region Methods

  private OneTimePassword(int[] value, DateTime expiry)
  {
    Value = value;
    Expiry = expiry;
  }

  public static OneTimePassword CreateNew()
  {
    Random random = new();
    int[] otp = new int[OTP_LENGTH];

    for (int i = 0; i < OTP_LENGTH; i++)
    {
      otp[i] = (char)random.Next(0, 10);
    }
    return new OneTimePassword(otp, DateTime.Now.AddMinutes(EXPIRY_MINUTES));
  }

  public static Result<OneTimePassword> Convert(int[] value, DateTime? expiry = null)
  {
    if (value.Length != OTP_LENGTH)
    {
      return InvalidOtpError.GetResult(nameof(value), "The OTP is not valid.");
    }

    if (expiry.HasValue && expiry.Value < DateTime.Now)
    {
      return ExpiryError.GetResult(nameof(expiry), "The expiry date is not valid.");
    }
    return new OneTimePassword(value, expiry ?? DateTime.Now.AddMinutes(EXPIRY_MINUTES));
  }

  public OneTimePassword WithExpiry(DateTime expiry)
  {
    Expiry = expiry;
    return this;
  }

  public Result Validate(OneTimePassword otp)
  {
    if (otp.Value.Length != OTP_LENGTH)
    {
      return InvalidOtpError.GetResult(nameof(otp), "The OTP is not valid.");
    }

    for (int i = 0; i < OTP_LENGTH; i++)
    {
      if (Value[i] != otp.Value[i])
      {
        return InvalidOtpError.GetResult(nameof(otp), "The OTP is not correct.");
      }
    }

    if (DateTime.Now > Expiry)
    {
      return ExpiryError.GetResult(nameof(otp), "The OTP has expired.");
    }

    return Result.Ok();
  }

  public static implicit operator string(OneTimePassword otp)
  {
    return string.Join("", otp.Value);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    foreach (var item in Value)
    {
      yield return item;
    }
    yield return Expiry;
  }

  #endregion
}
