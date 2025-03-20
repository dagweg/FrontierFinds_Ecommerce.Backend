using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.Entities;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Microsoft.AspNetCore.Identity;
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

    public static List<Domain.UserAggregate.User> GetSeed()
    {
      var otp = OneTimePassword.CreateNew();

      var passwordHasher = new PasswordHasher<Domain.UserAggregate.User>();
      // User Seeding
#pragma warning disable CS0618 // Type or member is obsolete
      return
      [
        Domain
          .UserAggregate.User.Create(
            firstName: Name.Create("John"),
            lastName: Name.Create("Doe"),
            email: Email.Create("johndoe@example.com"),
            password: Password
              .Create(
                passwordHasher.HashPassword(null!, "SecurePassword123!"),
                "SecurePassword123!"
              )
              .Value,
            phoneNumber: PhoneNumber.Create("+1234567890")
          )
          .WithAddress(
            UserAddress.Create(
              street: "123 Main St",
              city: "New York",
              state: "NY",
              zipCode: "10001",
              country: "US"
            )
          )
          .WithBillingAddress(
            BillingAddress.Create(
              street: "456 Elm St",
              city: "New York",
              state: "NY",
              zipCode: "10001",
              country: "US"
            )
          )
          .WithCart(Cart.Create())
          .WithAccountVerified(true)
          .WithEmailVerificationOtp(OneTimePassword.CreateNew())
          .WithPasswordResetOtp(OneTimePassword.CreateNew())
          .WithUserId(JohnDoeId),
        Domain
          .UserAggregate.User.Create(
            firstName: Name.Create("Emma"),
            lastName: Name.Create("Smith"),
            email: Email.Create("emmasmith@example.com"),
            password: Password
              .Create(
                passwordHasher.HashPassword(null!, "SecurePassword123!"),
                "SecurePassword123!"
              )
              .Value,
            phoneNumber: PhoneNumber.Create("+1987654321")
          )
          .WithAddress(
            UserAddress.Create(
              street: "123 Main St",
              city: "New York",
              state: "NY",
              zipCode: "10001",
              country: "US"
            )
          )
          .WithBillingAddress(
            BillingAddress.Create(
              street: "456 Elm St",
              city: "New York",
              state: "NY",
              zipCode: "10001",
              country: "US"
            )
          )
          .WithCart(Cart.Create())
          .WithAccountVerified(true)
          .WithEmailVerificationOtp(OneTimePassword.CreateNew()) // Using CreateNew here
          .WithPasswordResetOtp(OneTimePassword.CreateNew()) // Using CreateNew here
          .WithUserId(EmmaSmithId),
        Domain
          .UserAggregate.User.Create(
            firstName: Name.Create("Liam"),
            lastName: Name.Create("Johnson"),
            email: Email.Create("liamjohnson@example.com"),
            password: Password
              .Create(
                passwordHasher.HashPassword(null!, "SecurePassword123!"),
                "SecurePassword123!"
              )
              .Value,
            phoneNumber: PhoneNumber.Create("+1123456789")
          )
          .WithAddress(
            UserAddress.Create(
              street: "123 Main St",
              city: "New York",
              state: "NY",
              zipCode: "10001",
              country: "US"
            )
          )
          .WithBillingAddress(
            BillingAddress.Create(
              street: "456 Elm St",
              city: "New York",
              state: "NY",
              zipCode: "10001",
              country: "US"
            )
          )
          .WithCart(Cart.Create())
          .WithAccountVerified(true)
          .WithEmailVerificationOtp(OneTimePassword.CreateNew()) // Using CreateNew here
          .WithPasswordResetOtp(OneTimePassword.CreateNew())
          .WithUserId(LiamJohnsonId), // Using CreateNew here
        Domain
          .UserAggregate.User.Create(
            firstName: Name.Create("Olivia"),
            lastName: Name.Create("Brown"),
            email: Email.Create("oliviabrown@example.com"),
            password: Password
              .Create(passwordHasher.HashPassword(null!, "  "), "SecurePassword123!")
              .Value,
            phoneNumber: PhoneNumber.Create("+1098765432")
          )
          .WithAddress(
            UserAddress.Create(
              street: "123 Main St",
              city: "New York",
              state: "NY",
              zipCode: "10001",
              country: "US"
            )
          )
          .WithBillingAddress(
            BillingAddress.Create(
              street: "456 Elm St",
              city: "New York",
              state: "NY",
              zipCode: "10001",
              country: "US"
            )
          )
          .WithCart(Cart.Create())
          .WithAccountVerified(true)
          .WithEmailVerificationOtp(OneTimePassword.CreateNew()) // Using CreateNew here
          .WithPasswordResetOtp(OneTimePassword.CreateNew())
          .WithUserId(OliviaBrownId), // Using CreateNew here
      ];

#pragma warning restore CS0618 // Type or member is obsolete
    }
  }
}
