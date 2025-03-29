using System;
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

  [NotMapped]
  public const int RESEND_DELAY_MULTIPLIER = 3;

  #endregion

  #region Properties

  public int[]? Value { get; private set; }
  public DateTime Expiry { get; private set; }
  public DateTime NextResendValidAt { get; private set; }

  public int ResendFailStreak { get; private set; } = 0; // for tracking resend attempts and increasing delay over time
  #endregion


  #region Methods

  private OneTimePassword(int[] value, DateTime expiry)
  {
    Value = value;
    Expiry = expiry;
    NextResendValidAt = DateTime.MinValue; // valid for resending immediately
  }

  public static OneTimePassword CreateNew()
  {
    Random random = new();
    int[] otp = new int[OTP_LENGTH];

    for (int i = 0; i < OTP_LENGTH; i++)
    {
      otp[i] = (char)random.Next(0, 10);
    }
    return new OneTimePassword(otp, DateTime.UtcNow.AddMinutes(EXPIRY_MINUTES));
  }

  public static Result<OneTimePassword> Convert(int[] value, DateTime? expiry = null)
  {
    if (value.Length != OTP_LENGTH)
    {
      return InvalidOtpError.GetResult(nameof(value), "The OTP is not valid.");
    }

    if (expiry.HasValue && expiry.Value < DateTime.UtcNow)
    {
      return ExpiryError.GetResult(nameof(expiry), "The expiry date is not valid.");
    }
    return new OneTimePassword(value, expiry ?? DateTime.UtcNow.AddMinutes(EXPIRY_MINUTES));
  }

  public OneTimePassword WithExpiry(DateTime expiry)
  {
    Expiry = expiry;
    return this;
  }

  public void SetValue(int[]? value, DateTime? expiry = null)
  {
    Value = value ?? Value;
    Expiry = expiry ?? DateTime.UtcNow.AddMinutes(EXPIRY_MINUTES);
  }

  public void Revoke()
  {
    Value = new int[OTP_LENGTH];
    Expiry = DateTime.MinValue;
  }

  public Result AddResendDelay(int mul = 1)
  {
    NextResendValidAt = DateTime.UtcNow.AddMinutes(
      0.1 * mul * (ResendFailStreak + 1) * RESEND_DELAY_MULTIPLIER
    );
    ResendFailStreak++;
    return Result.Ok();
  }

  public Result Validate(OneTimePassword otp)
  {
    if (otp?.Value?.Length != OTP_LENGTH)
    {
      return InvalidOtpError.GetResult(nameof(otp), "The OTP is not valid.");
    }

    for (int i = 0; i < OTP_LENGTH; i++)
    {
      if (Value?[i] != otp.Value[i])
      {
        return InvalidOtpError.GetResult(nameof(otp), "The OTP is not correct.");
      }
    }

    if (DateTime.UtcNow > Expiry)
    {
      return ExpiryError.GetResult(nameof(otp), "The OTP has expired.");
    }

    return Result.Ok();
  }

  public static implicit operator string(OneTimePassword otp) => otp.ToString();

  public override string ToString()
  {
    return string.Join("", Value ?? []);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    if (Value is not null)
      foreach (var item in Value)
      {
        yield return item;
      }
    yield return Expiry;
  }

  #endregion
}
