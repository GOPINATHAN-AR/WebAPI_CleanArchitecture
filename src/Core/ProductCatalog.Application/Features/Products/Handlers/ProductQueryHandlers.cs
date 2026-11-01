using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.Common.Mappings;
using ProductCatalog.Application.Common.Models;
using ProductCatalog.Application.Features.Products.DTOs;
using ProductCatalog.Application.Features.Products.Queries;

namespace ProductCatalog.Application.Features.Products.Handlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    private readonly IApplicationDbContext _context;

    public GetProductByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);
            
            if (product == null)
                return Result<ProductDto>.Failure("Product not found");

            var productDto = product.ToDto();
            return Result<ProductDto>.Success(productDto);
        }
        catch (Exception ex)
        {
            return Result<ProductDto>.Failure($"Error retrieving product: {ex.Message}");
        }
    }
}

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<PagedResult<ProductDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetAllProductsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedResult<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _context.Products.AsQueryable();

            var totalCount = await query.CountAsync(cancellationToken);
            
            var products = await query
                .Skip((request.Pagination.PageNumber - 1) * request.Pagination.PageSize)
                .Take(request.Pagination.PageSize)
                .ToListAsync(cancellationToken);

            var productDtos = products.Select(p => p.ToDto()).ToList();

            var pagedResult = new PagedResult<ProductDto>
            {
                Items = productDtos,
                TotalCount = totalCount,
                PageNumber = request.Pagination.PageNumber,
                PageSize = request.Pagination.PageSize
            };

            return Result<PagedResult<ProductDto>>.Success(pagedResult);
        }
        catch (Exception ex)
        {
            return Result<PagedResult<ProductDto>>.Failure($"Error retrieving products: {ex.Message}");
        }
    }
}

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<PagedResult<ProductDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetProductsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedResult<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _context.Products.AsQueryable();

            // Apply filters
            if (!string.IsNullOrWhiteSpace(request.Filter.Name))
                query = query.Where(p => p.Name.Contains(request.Filter.Name));

            if (!string.IsNullOrWhiteSpace(request.Filter.Category))
                query = query.Where(p => p.Category == request.Filter.Category);

            if (!string.IsNullOrWhiteSpace(request.Filter.Brand))
                query = query.Where(p => p.Brand == request.Filter.Brand);

            if (request.Filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= request.Filter.MinPrice);

            if (request.Filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= request.Filter.MaxPrice);

            if (request.Filter.Status.HasValue)
                query = query.Where(p => p.Status == request.Filter.Status);

            if (request.Filter.IsActive.HasValue)
                query = query.Where(p => p.IsActive == request.Filter.IsActive);

            var totalCount = await query.CountAsync(cancellationToken);
            
            var products = await query
                .Skip((request.Pagination.PageNumber - 1) * request.Pagination.PageSize)
                .Take(request.Pagination.PageSize)
                .ToListAsync(cancellationToken);

            var productDtos = products.Select(p => p.ToDto()).ToList();

            var pagedResult = new PagedResult<ProductDto>
            {
                Items = productDtos,
                TotalCount = totalCount,
                PageNumber = request.Pagination.PageNumber,
                PageSize = request.Pagination.PageSize
            };

            return Result<PagedResult<ProductDto>>.Success(pagedResult);
        }
        catch (Exception ex)
        {
            return Result<PagedResult<ProductDto>>.Failure($"Error retrieving products: {ex.Message}");
        }
    }
}

public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, Result<PagedResult<ProductDto>>>
{
    private readonly IApplicationDbContext _context;

    public SearchProductsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedResult<ProductDto>>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower();
                query = query.Where(p => 
                    p.Name.ToLower().Contains(searchTerm) ||
                    p.Description.ToLower().Contains(searchTerm) ||
                    p.Category.ToLower().Contains(searchTerm) ||
                    (p.Brand != null && p.Brand.ToLower().Contains(searchTerm)) ||
                    (p.Sku != null && p.Sku.ToLower().Contains(searchTerm))
                );
            }

            var totalCount = await query.CountAsync(cancellationToken);
            
            var products = await query
                .Skip((request.Pagination.PageNumber - 1) * request.Pagination.PageSize)
                .Take(request.Pagination.PageSize)
                .ToListAsync(cancellationToken);

            var productDtos = products.Select(p => p.ToDto()).ToList();

            var pagedResult = new PagedResult<ProductDto>
            {
                Items = productDtos,
                TotalCount = totalCount,
                PageNumber = request.Pagination.PageNumber,
                PageSize = request.Pagination.PageSize
            };

            return Result<PagedResult<ProductDto>>.Success(pagedResult);
        }
        catch (Exception ex)
        {
            return Result<PagedResult<ProductDto>>.Failure($"Error searching products: {ex.Message}");
        }
    }
}
