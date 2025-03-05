using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Contracts.Authentication;

public record LoginRequest([Required][EmailAddress] string Email, [Required] string Password);
