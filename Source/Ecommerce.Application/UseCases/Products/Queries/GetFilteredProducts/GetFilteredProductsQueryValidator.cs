using FluentValidation;

namespace Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;

public class GetFilteredProductsQueryValidator : AbstractValidator<FilterProductsQuery>
{
  public GetFilteredProductsQueryValidator() { }
}
