using System.ComponentModel.DataAnnotations;
using Ecommerce.Api.Attributes;

namespace Ecommerce.Contracts.Authentication;

public record VerifyAccountRequest
{
  [Required]
  public string Otp { get; init; } = null!;

  [Required]
  public string UserId { get; init; } = null!;
}
