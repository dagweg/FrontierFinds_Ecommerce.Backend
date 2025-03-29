using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Images.Common;
using Ecommerce.Application.UseCases.Products.Common;

namespace Ecommerce.Application.UseCases.Users.Common;

public class UserResult
{
  public required string UserId { get; set; }
  public required string FirstName { get; set; }
  public required string LastName { get; set; }
  public required string Email { get; set; }
  public required string PhoneNumber { get; set; }
  public AddressResult? Address { get; set; }
  public bool AccountVerified { get; set; }
  public ImageResult? ProfileImage { get; set; }
}
