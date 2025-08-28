using ProductCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalog.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Category> Categories { get; }
    DbSet<ProductCategory> ProductCategories { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
