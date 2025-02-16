namespace Ecommerce.Application.UseCases.Common.Interfaces;

public interface IPaginated
{
  int PageNumber { get; }
  int PageSize { get; }
}
