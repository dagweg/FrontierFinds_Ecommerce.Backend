namespace Ecommerce.Application.Common.Models.Storage;

public class UploadFileParams
{
  /// <summary>
  ///  Used to locate the resource
  /// </summary>
  public string? ObjectIdentifier { get; set; }

  public required Stream FileStream { get; set; }

  public string? FileName { get; set; }
}
