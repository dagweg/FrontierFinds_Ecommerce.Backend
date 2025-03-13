using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure;

public static partial class Seeding
{
  public static class User
  {
    // Define static fields for User IDs
    public static readonly UserId JohnDoeId = UserId.Convert(
      Guid.Parse("c49b9246-e6ba-45b7-a9b9-f302f21eed4d")
    );
    public static readonly UserId EmmaSmithId = UserId.Convert(
      Guid.Parse("d5e3b1a2-4f8e-4b2a-9c1d-3e8f5a6b7c9d")
    );
    public static readonly UserId LiamJohnsonId = UserId.Convert(
      Guid.Parse("e7f4c2b3-5f9f-4c3b-a2e1-4f9f6b7c8d0e")
    );
    public static readonly UserId OliviaBrownId = UserId.Convert(
      Guid.Parse("f8f5d3c4-6f0f-4d4c-b3f2-5f0f7c8d9e1f")
    );

    public static void Seed(ModelBuilder builder)
    {
      // User Seeding
      builder
        .Entity<Domain.UserAggregate.User>()
        .HasData(
          new
          {
            Id = JohnDoeId, // Use the static field
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@example.com",
            Password = "SecurePassword123!",
            PhoneNumber = "+1234567890",
            AccountVerified = true,
            Address_City = "New York",
            Address_State = "NY",
            Address_Street = "123 Main St",
            Address_ZipCode = "10001",
            Address_Country = "US",
            BillingAddress_Country = "US",
            BillingAddress_State = "NY",
            BillingAddress_City = "New York",
            BillingAddress_Street = "456 Elm St",
            BillingAddress_ZipCode = "10001",
            EmailVerificationOtp_Value = "123456",
            EmailVerificationOtp_Expiry = new DateTime(2025, 03, 14, 0, 0, 0),
            EmailVerificationOtpNextResendValidAt = (DateTime?)null,
            EmailVerificationOtp_ResendFailStreak = 0,
            PasswordResetOtp_Value = "654321",
            PasswordResetOtp_Expiry = new DateTime(2025, 03, 14, 0, 0, 0),
            PasswordResetOtpNextResendValidAt = (DateTime?)null,
            PasswordResetOtp_ResendFailStreak = 0,
          },
          new
          {
            Id = EmmaSmithId, // Use the static field
            FirstName = "Emma",
            LastName = "Smith",
            Email = "emmasmith@example.com",
            Password = "Password456!",
            PhoneNumber = "+1987654321",
            AccountVerified = true,
            Address_City = "New York",
            Address_State = "NY",
            Address_Street = "123 Main St",
            Address_ZipCode = "10001",
            Address_Country = "US",
            BillingAddress_Country = "US",
            BillingAddress_State = "NY",
            BillingAddress_City = "New York",
            BillingAddress_Street = "456 Elm St",
            BillingAddress_ZipCode = "10001",
            EmailVerificationOtp_Value = "123456",
            EmailVerificationOtp_Expiry = new DateTime(2025, 03, 14, 0, 0, 0),
            EmailVerificationOtpNextResendValidAt = (DateTime?)null,
            EmailVerificationOtp_ResendFailStreak = 0,
            PasswordResetOtp_Value = "654321",
            PasswordResetOtp_Expiry = new DateTime(2025, 03, 14, 0, 0, 0),
            PasswordResetOtpNextResendValidAt = (DateTime?)null,
            PasswordResetOtp_ResendFailStreak = 0,
          },
          new
          {
            Id = LiamJohnsonId, // Use the static field
            FirstName = "Liam",
            LastName = "Johnson",
            Email = "liamjohnson@example.com",
            Password = "Liam789!",
            PhoneNumber = "+1123456789",
            AccountVerified = true,
            Address_City = "New York",
            Address_State = "NY",
            Address_Street = "123 Main St",
            Address_ZipCode = "10001",
            Address_Country = "US",
            BillingAddress_Country = "US",
            BillingAddress_State = "NY",
            BillingAddress_City = "New York",
            BillingAddress_Street = "456 Elm St",
            BillingAddress_ZipCode = "10001",
            EmailVerificationOtp_Value = "123456",
            EmailVerificationOtp_Expiry = new DateTime(2025, 03, 14, 0, 0, 0),
            EmailVerificationOtpNextResendValidAt = (DateTime?)null,
            EmailVerificationOtp_ResendFailStreak = 0,
            PasswordResetOtp_Value = "654321",
            PasswordResetOtp_Expiry = new DateTime(2025, 03, 14, 0, 0, 0),
            PasswordResetOtpNextResendValidAt = (DateTime?)null,
            PasswordResetOtp_ResendFailStreak = 0,
          },
          new
          {
            Id = OliviaBrownId, // Use the static field
            FirstName = "Olivia",
            LastName = "Brown",
            Email = "oliviabrown@example.com",
            Password = "Olivia101!",
            PhoneNumber = "+1098765432",
            AccountVerified = true,
            Address_City = "New York",
            Address_State = "NY",
            Address_Street = "123 Main St",
            Address_ZipCode = "10001",
            Address_Country = "US",
            BillingAddress_Country = "US",
            BillingAddress_State = "NY",
            BillingAddress_City = "New York",
            BillingAddress_Street = "456 Elm St",
            BillingAddress_ZipCode = "10001",
            EmailVerificationOtp_Value = "123456",
            EmailVerificationOtp_Expiry = new DateTime(2025, 03, 14, 0, 0, 0),
            EmailVerificationOtpNextResendValidAt = (DateTime?)null,
            EmailVerificationOtp_ResendFailStreak = 0,
            PasswordResetOtp_Value = "654321",
            PasswordResetOtp_Expiry = new DateTime(2025, 03, 14, 0, 0, 0),
            PasswordResetOtpNextResendValidAt = (DateTime?)null,
            PasswordResetOtp_ResendFailStreak = 0,
          }
        );
    }

    public static class Cart
    {
      public static void Seed(ModelBuilder builder)
      {
        // UserCarts Seeding
        builder
          .Entity<Domain.UserAggregate.Entities.Cart>()
          .HasData(
            new
            {
              UserId = JohnDoeId, // Use the static field
              CartId = Guid.NewGuid(),
            },
            new
            {
              UserId = EmmaSmithId, // Use the static field
              CartId = Guid.NewGuid(),
            },
            new
            {
              UserId = LiamJohnsonId, // Use the static field
              CartId = Guid.NewGuid(),
            },
            new
            {
              UserId = OliviaBrownId, // Use the static field
              CartId = Guid.NewGuid(),
            }
          );
      }
    }
  }
}
