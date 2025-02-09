using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Contracts.User;

public class ResetPasswordVerifyRequest
{
  [Required]
  [MinLength(1)]
  public string Otp { get; set; } = null!;

  [Required]
  [MinLength(1)]
  public string NewPassword { get; set; } = null!;

  [Required]
  [MinLength(1)]
  public string ConfirmNewPassword { get; set; } = null!;
}
