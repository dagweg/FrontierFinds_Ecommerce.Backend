using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Attributes;

public class GuidAttribute : ValidationAttribute
{
  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
  {
    if (value is null)
    {
      return new ValidationResult("Id cannot be null");
    }

    if (value is not string id)
    {
      return new ValidationResult("Id must be a string");
    }

    if (!Guid.TryParse(id, out var _))
    {
      return new ValidationResult("Id must be a valid GUID string.");
    }

    return ValidationResult.Success;
  }
}
