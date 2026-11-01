using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.Common.Mappings;
using ProductCatalog.Application.Common.Models;
using ProductCatalog.Application.Features.Products.Commands;
using ProductCatalog.Application.Features.Products.DTOs;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Features.Products.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductDto>>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Product.Name,
                Description = request.Product.Description,
                Price = request.Product.Price,
                Currency = request.Product.Currency,
                StockQuantity = request.Product.StockQuantity,
                Category = request.Product.Category,
                Brand = request.Product.Brand,
                Sku = request.Product.Sku,
                Weight = request.Product.Weight,
                ImageUrl = request.Product.ImageUrl,
                IsActive = true,
                Status = ProductStatus.Draft,
                CreatedAt = DateTime.UtcNow
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            var productDto = product.ToDto();
            return Result<ProductDto>.Success(productDto);
        }
        catch (Exception ex)
        {
            return Result<ProductDto>.Failure($"Error creating product: {ex.Message}");
        }
    }
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);
            
            if (product == null)
                return Result<bool>.Failure("Product not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Error deleting product: {ex.Message}");
        }
    }
}

public class UpdateProductStatusCommandHandler : IRequestHandler<UpdateProductStatusCommand, Result<ProductDto>>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductStatusCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ProductDto>> Handle(UpdateProductStatusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);
            
            if (product == null)
                return Result<ProductDto>.Failure("Product not found");

            product.Status = request.Status;
            product.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            var productDto = product.ToDto();
            return Result<ProductDto>.Success(productDto);
        }
        catch (Exception ex)
        {
            return Result<ProductDto>.Failure($"Error updating product status: {ex.Message}");
        }
    }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<ProductDto>>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);
            
            if (product == null)
                return Result<ProductDto>.Failure("Product not found");

            product.Name = request.Product.Name;
            product.Description = request.Product.Description;
            product.Price = request.Product.Price;
            product.Currency = request.Product.Currency;
            product.StockQuantity = request.Product.StockQuantity;
            product.Category = request.Product.Category;
            product.Brand = request.Product.Brand;
            product.Sku = request.Product.Sku;
            product.Weight = request.Product.Weight;
            product.ImageUrl = request.Product.ImageUrl;
            product.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            var productDto = product.ToDto();
            return Result<ProductDto>.Success(productDto);
        }
        catch (Exception ex)
        {
            return Result<ProductDto>.Failure($"Error updating product: {ex.Message}");
        }
    }
}

public class ActivateProductCommandHandler : IRequestHandler<ActivateProductCommand, Result<ProductDto>>
{
    private readonly IApplicationDbContext _context;

    public ActivateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ProductDto>> Handle(ActivateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);
            
            if (product == null)
                return Result<ProductDto>.Failure("Product not found");

            product.IsActive = true;
            product.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            var productDto = product.ToDto();
            return Result<ProductDto>.Success(productDto);
        }
        catch (Exception ex)
        {
            return Result<ProductDto>.Failure($"Error activating product: {ex.Message}");
        }
    }
}

public class DeactivateProductCommandHandler : IRequestHandler<DeactivateProductCommand, Result<ProductDto>>
{
    private readonly IApplicationDbContext _context;

    public DeactivateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ProductDto>> Handle(DeactivateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);
            
            if (product == null)
                return Result<ProductDto>.Failure("Product not found");

            product.IsActive = false;
            product.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            var productDto = product.ToDto();
            return Result<ProductDto>.Success(productDto);
        }
        catch (Exception ex)
        {
            return Result<ProductDto>.Failure($"Error deactivating product: {ex.Message}");
        }
    }
}
