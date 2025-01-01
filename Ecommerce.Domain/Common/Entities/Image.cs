using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.Common.Entities;

public class Image : Entity<Guid>
{
  public string Url { get; protected set; }
  public string? FileType { get; protected set; }
  public string? FileName { get; protected set; }
  public long? FileSize { get; protected set; }

  protected Image(Guid id, string url, string? fileType, string? fileName, long? fileSize)
    : base(id)
  {
    Url = url;
    FileName = fileName;
    FileSize = fileSize;
    FileType = fileType;
  }

  public static Image Create(
    string url,
    string fileType = "",
    string fileName = "",
    long fileSize = 0
  ) => new(Guid.NewGuid(), url, fileType, fileName, fileSize);

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  protected Image()
    : base(Guid.Empty) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}
