using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Infrastructure.Services.Storage;

public class CloudinarySettings
{
    public const string SectionName = nameof(CloudinarySettings);

    public required string ApiKey { get; set; }
    public required string ApiSecret { get; set; }
    public required string CloudName { get; set; }
    public required string Folder { get; set; }

    public string GetUrl => $"cloudinary://{ApiKey}:{ApiSecret}@{CloudName}";
}
