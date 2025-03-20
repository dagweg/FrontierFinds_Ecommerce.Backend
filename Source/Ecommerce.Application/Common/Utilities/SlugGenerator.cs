using System.Text.RegularExpressions;

namespace Ecommerce.Application.Common.Utilities;

public static class SlugGenerator
{
  public static string GenerateSlug(string input)
  {
    if (string.IsNullOrWhiteSpace(input))
      return string.Empty;

    // Convert to lowercase and trim
    string slug = input.ToLowerInvariant().Trim();

    // Replace spaces with hyphens
    slug = slug.Replace(" ", "-");

    // Remove invalid characters (keep letters, numbers, and hyphens)
    slug = Regex.Replace(slug, "[^a-z0-9-]", "");

    // Replace multiple hyphens with a single hyphen
    slug = Regex.Replace(slug, "-+", "-");

    // Remove leading/trailing hyphens
    slug = slug.Trim('-');

    return slug;
  }
}
