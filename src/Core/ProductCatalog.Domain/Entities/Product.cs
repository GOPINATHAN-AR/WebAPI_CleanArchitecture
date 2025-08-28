using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Entities;

public class Product : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = "USD";
    public int StockQuantity { get; set; }
    public string Category { get; set; } = string.Empty;
    public string? Brand { get; set; }
    public string? Sku { get; set; }
    public bool IsActive { get; set; } = true;
    public decimal? Weight { get; set; }
    public string? ImageUrl { get; set; }
    public ProductStatus Status { get; set; } = ProductStatus.Draft;
    
    // Navigation properties
    public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
}

public class Category : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public ICollection<Category> SubCategories { get; set; } = new List<Category>();
    public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
}

public class ProductCategory : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}

public enum ProductStatus
{
    Draft = 0,
    Published = 1,
    OutOfStock = 2,
    Discontinued = 3
}
