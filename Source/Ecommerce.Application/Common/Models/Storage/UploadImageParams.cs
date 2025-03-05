namespace Ecommerce.Application.Common.Models.Storage;

public class UploadImageParams
{
    /// <summary>
    ///  Used to locate the resource
    /// </summary>
    public string? ObjectIdentifier { get; set; }

    public required Stream ImageStream { get; set; }

    public string? FileName { get; set; }
}
