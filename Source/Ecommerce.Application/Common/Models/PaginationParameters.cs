using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Application.UseCases.Common.Interfaces;

namespace Ecommerce.Application.Common.Models
{
  public class PaginationParameters
  {
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
  }

  public record PaginationParametersImmutable
  {
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
  }
}
