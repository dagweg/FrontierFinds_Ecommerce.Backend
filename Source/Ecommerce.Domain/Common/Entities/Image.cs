using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.Common.Entities;

public class Image : ValueObject
{
    public string Url { get; protected set; }
    public string ObjectIdentifier { get; protected set; }

    [NotMapped]
    public const int SIZE_LIMIT_BYTES = 3 * 1024 * 1024; // 3MB

    protected Image(string url, string objectIdentifier)
    {
        Url = url;
        ObjectIdentifier = objectIdentifier;
    }

    public static Image Create(string url, string objectIdentifier) => new(url, objectIdentifier);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Url;
        yield return ObjectIdentifier;
    }
}
