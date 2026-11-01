using ProductCatalog.Application.Features.Products.DTOs;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Common.Mappings;

public static class ProductMappings
{
    public static ProductDto ToDto(this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Currency = product.Currency,
            StockQuantity = product.StockQuantity,
            Category = product.Category,
            Brand = product.Brand,
            Sku = product.Sku,
            IsActive = product.IsActive,
            Weight = product.Weight,
            ImageUrl = product.ImageUrl,
            Status = product.Status,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }
}
