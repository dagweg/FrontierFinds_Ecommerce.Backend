using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Application.UseCases.Common.Interfaces;

namespace Ecommerce.Application.Common.Models
{
    public class PaginationParameters(int pageNumber = 1, int pageSize = 10) : IPaginated
    {
        public int PageNumber { get; init; } = pageNumber;
        public int PageSize { get; init; } = pageSize;
    }
}
