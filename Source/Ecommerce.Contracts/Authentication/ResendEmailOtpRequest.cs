using System.ComponentModel.DataAnnotations;
using Ecommerce.Api.Attributes;

namespace Ecommerce.Contracts.Authentication
{
  public class ResendEmailOtpRequest
  {
    [Required]
    [Guid]
    public string UserId { get; set; } = null!;
  }
}
