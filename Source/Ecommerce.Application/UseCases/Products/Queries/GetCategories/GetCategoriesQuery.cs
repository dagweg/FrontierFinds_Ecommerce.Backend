using Ecommerce.Application.UseCases.Products.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Queries.GetCategories;

public record GetCategoriesQuery() : IRequest<Result<CategoriesResult>>;
