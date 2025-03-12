using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class GuidListAttribute : ValidationAttribute
{
  private readonly string _errorMessage;

  public GuidListAttribute(string errorMessage = "All items must be valid GUIDs.")
  {
    _errorMessage = errorMessage;
  }

  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
  {
    // Ensure the value is a collection of strings (e.g., List<string>, string[])
    if (value is not IEnumerable<string> items)
    {
      return new ValidationResult("Value must be a collection of strings.");
    }

    foreach (var item in items)
    {
      if (string.IsNullOrWhiteSpace(item) || !Guid.TryParse(item, out _))
      {
        return new ValidationResult(_errorMessage);
      }
    }

    return ValidationResult.Success;
  }
}
