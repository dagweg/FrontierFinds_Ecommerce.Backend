namespace Ecommerce.Contracts.Common;

/// <summary>
/// Pagination parameters
/// </summary>
/// <param name="PageNumber"></param>
/// <param name="PageSize"></param>
public record PaginationParams(int PageNumber = 1, int PageSize = 10);
