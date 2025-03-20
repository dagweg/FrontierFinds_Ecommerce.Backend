using Ecommerce.Application.UseCases.Products.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Queries.GetProductBySlug;

public record GetProductBySlugQuery(string Slug) : IRequest<Result<ProductResult>>;
