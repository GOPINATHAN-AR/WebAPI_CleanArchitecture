using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Features.Products.DTOs;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public string Category { get; set; } = string.Empty;
    public string? Brand { get; set; }
    public string? Sku { get; set; }
    public bool IsActive { get; set; }
    public decimal? Weight { get; set; }
    public string? ImageUrl { get; set; }
    public ProductStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CreateProductDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = "USD";
    public int StockQuantity { get; set; }
    public string Category { get; set; } = string.Empty;
    public string? Brand { get; set; }
    public string? Sku { get; set; }
    public decimal? Weight { get; set; }
    public string? ImageUrl { get; set; }
}

public class UpdateProductDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = "USD";
    public int StockQuantity { get; set; }
    public string Category { get; set; } = string.Empty;
    public string? Brand { get; set; }
    public string? Sku { get; set; }
    public decimal? Weight { get; set; }
    public string? ImageUrl { get; set; }
}

public class ProductQueryDto
{
    public string? Name { get; set; }
    public string? Category { get; set; }
    public string? Brand { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public ProductStatus? Status { get; set; }
    public bool? IsActive { get; set; }
}
