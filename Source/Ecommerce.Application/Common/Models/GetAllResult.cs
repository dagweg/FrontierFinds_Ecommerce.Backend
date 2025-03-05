namespace Ecommerce.Application.Common.Models;

public class GetAllResult<TObject>
{
    public IEnumerable<TObject> Items { get; set; } = [];
    public required int TotalItems { get; set; }
    public required int TotalItemsFetched { get; set; }
}
