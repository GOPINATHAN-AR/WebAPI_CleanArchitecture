using MediatR;
using ProductCatalog.Application.Common.Models;
using ProductCatalog.Application.Features.Products.DTOs;

namespace ProductCatalog.Application.Features.Products.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductDto>>;

public record GetAllProductsQuery(PaginationQuery Pagination) : IRequest<Result<PagedResult<ProductDto>>>;

public record GetProductsQuery(ProductQueryDto Filter, PaginationQuery Pagination) : IRequest<Result<PagedResult<ProductDto>>>;

public record SearchProductsQuery(string SearchTerm, PaginationQuery Pagination) : IRequest<Result<PagedResult<ProductDto>>>;
