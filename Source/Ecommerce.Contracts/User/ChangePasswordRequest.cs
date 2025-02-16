using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Contracts.User;

public class ChangePasswordRequest
{
  [Required]
  public string CurrentPassword { get; set; } = null!;

  [Required]
  public string NewPassword { get; set; } = null!;
}
