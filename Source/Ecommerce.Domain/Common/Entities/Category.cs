using System.Diagnostics.CodeAnalysis;
using System.Text;
using Ecommerce.Domain.Common.Models;
using FluentResults;

namespace Ecommerce.Domain.Common.Entities;

public class Category : Entity<int>
{
  public string Name { get; private set; }

  public string Slug { get; private set; }

  public Category? Parent { get; private set; }
  public int? ParentId { get; private set; }

  private List<Category> _subCategories = [];
  public IReadOnlyCollection<Category> SubCategories => _subCategories.AsReadOnly();

  public bool IsActive { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  private Category() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

  private Category(int id, string name, string slug, Category? parent = null, bool isActive = true)
    : base(id)
  {
    Name = name;
    Slug = slug;
    Parent = parent;
    ParentId = parent?.Id;
    IsActive = isActive;
  }

  public static Result<Category> Create(
    int id,
    string name,
    string slug,
    Category? parent = null,
    bool isActive = true
  )
  {
    return new Category(id, name, Slugify(slug), parent, isActive);
  }

  public static string Slugify(string slug)
  {
    return slug.ToLower()
      .Replace(" ", "-")
      .Replace("--", "-")
      .Where(ch => char.IsLetterOrDigit(ch) || ch == '-')
      .Aggregate(new StringBuilder(), (sb, ch) => sb.Append(ch))
      .ToString()
      .Trim('-');
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }
}
