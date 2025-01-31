namespace Ecommerce.Application.UseCases.Common.Interfaces;

public interface IPaginated
{
  int PageNumber { get; init; }
  int PageSize { get; init; }
}
