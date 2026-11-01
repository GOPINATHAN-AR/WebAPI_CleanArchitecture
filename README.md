# Product Catalog API - Clean Architecture

This is a Web API project implementing Clean Architecture principles for product catalog management with full CRUD operations.

## Architecture Overview

The solution follows Clean Architecture principles with the following layers:

### Core Layer
#### Domain (`ProductCatalog.Domain`)
- **Entities**: Core business entities (Product, Category, ProductCategory). Only Product is exposed through the API
- **Enums**: Product status enumeration
- **Common**: Base entities and shared types

#### Application (`ProductCatalog.Application`)
- **Features**: Feature-based organization with Commands, Queries, DTOs, Handlers, and Validators
- **Common**: Shared interfaces, models, and mappings
- **CQRS**: Command Query Responsibility Segregation using MediatR

### Infrastructure Layer (`ProductCatalog.Infrastructure`)
- **Data**: Entity Framework Core DbContext and configurations
- **External Services**: Integration with external systems (future)

### Presentation Layer (`ProductCatalog.API`)
- **Controllers**: REST API endpoints
- **Configuration**: Dependency injection and middleware setup

## Key Clean Architecture Concepts Implemented

1. **Dependency Inversion**: Domain and Application don't reference the Infrastructure or API projects. Application reaches the database through `IApplicationDbContext`, which uses EF Core types
2. **Separation of Concerns**: Each layer has distinct responsibilities
3. **Feature-based Organization**: Code organized by business features
4. **CQRS**: Commands and queries are separated
5. **Result Pattern**: Consistent error handling across the application
6. **Pagination**: Built-in pagination support for list operations
7. **Explicit Mapping**: Hand-written entity-to-DTO mapping
8. **FluentValidation**: Validators run on every request through a MediatR pipeline behavior

## Features

- Complete CRUD operations for products
- Filtering and search
- Pagination support for all list operations
- Product status management (Draft, Published, OutOfStock, Discontinued)
- Product activation/deactivation
- Validation on create and update, with a 400 response listing the errors per field
- In-memory database for development
- Swagger documentation

## API Endpoints

### Products
- `GET /api/products` - Get all products with pagination
- `GET /api/products/{id}` - Get product by ID
- `GET /api/products/search?searchTerm={term}` - Search products
- `POST /api/products/filter` - Get filtered products
- `POST /api/products` - Create new product
- `PUT /api/products/{id}` - Update product
- `PATCH /api/products/{id}/activate` - Activate product
- `PATCH /api/products/{id}/deactivate` - Deactivate product
- `PATCH /api/products/{id}/status` - Update product status (the body is the bare status number, e.g. `1`)
- `DELETE /api/products/{id}` - Delete product

## Running the Application

1. Navigate to the API project directory:
   ```bash
   cd src/Presentation/ProductCatalog.API
   ```

2. Run the application in the Development environment, which is what enables Swagger:
   ```bash
   dotnet run -- --environment Development
   ```

3. Access Swagger UI at: `http://localhost:5000/swagger`

## Sample Request Bodies

### Create Product
Each object below is a separate example. Send one per request.

```json
{
  "name": "Smartphone",
  "description": "High-end smartphone with advanced features",
  "price": 899.99,
  "currency": "USD",
  "stockQuantity": 100,
  "category": "Electronics",
  "brand": "TechBrand",
  "sku": "TB-SP-001",
  "weight": 0.2,
  "imageUrl": "https://example.com/images/smartphone.jpg"
}

{
  "name": "Laptop Pro",
  "description": "Powerful laptop with 16GB RAM and SSD storage",
  "price": 1299.99,
  "currency": "USD",
  "stockQuantity": 50,
  "category": "Electronics",
  "brand": "TechBrand",
  "sku": "TB-LP-002",
  "weight": 1.5,
  "imageUrl": "https://example.com/images/laptop-pro.jpg"
}

{
  "name": "Wireless Headphones",
  "description": "Noise-cancelling over-ear headphones with long battery life",
  "price": 199.99,
  "currency": "USD",
  "stockQuantity": 200,
  "category": "Audio",
  "brand": "SoundWave",
  "sku": "SW-HP-003",
  "weight": 0.3,
  "imageUrl": "https://example.com/images/headphones.jpg"
}

{
  "name": "Smartwatch",
  "description": "Fitness tracker with heart rate monitor and GPS",
  "price": 249.99,
  "currency": "USD",
  "stockQuantity": 150,
  "category": "Wearables",
  "brand": "FitTech",
  "sku": "FT-SW-004",
  "weight": 0.05,
  "imageUrl": "https://example.com/images/smartwatch.jpg"
}

{
  "name": "4K Smart TV",
  "description": "55-inch 4K Ultra HD smart TV with streaming apps",
  "price": 599.99,
  "currency": "USD",
  "stockQuantity": 30,
  "category": "Electronics",
  "brand": "VisionTech",
  "sku": "VT-TV-005",
  "weight": 12.0,
  "imageUrl": "https://example.com/images/smart-tv.jpg"
}

{
  "name": "Bluetooth Speaker",
  "description": "Portable waterproof speaker with 360-degree sound",
  "price": 79.99,
  "currency": "USD",
  "stockQuantity": 300,
  "category": "Audio",
  "brand": "SoundWave",
  "sku": "SW-BS-006",
  "weight": 0.5,
  "imageUrl": "https://example.com/images/bluetooth-speaker.jpg"
}
```

### Update Product
```json
{
  "name": "Updated Smartphone",
  "description": "Updated high-end smartphone with latest features",
  "price": 849.99,
  "currency": "USD",
  "stockQuantity": 150,
  "category": "Electronics",
  "brand": "TechBrand",
  "sku": "TB-SP-001-V2",
  "weight": 0.18,
  "imageUrl": "https://example.com/images/smartphone-v2.jpg"
}
```

### Filter Products
```json
{
  "name": "phone",
  "category": "Electronics",
  "brand": "TechBrand",
  "minPrice": 500,
  "maxPrice": 1000,
  "status": 1,
  "isActive": true
}
```

### Pagination Parameters
```
?pageNumber=1&pageSize=10
```

## Validation Errors

Invalid requests return `400 Bad Request` with one entry per failed rule:

```json
[
  "Price: Product price must be greater than zero",
  "Currency: Currency must be 3 characters long"
]
```

## Product Status Values
- `0` - Draft
- `1` - Published
- `2` - OutOfStock
- `3` - Discontinued

## Architecture Benefits

1. **Testability**: The layers are separated so they can be tested independently (no tests are written yet)
2. **Maintainability**: Clear separation of concerns makes code easier to maintain
3. **Flexibility**: Infrastructure sits behind an interface, and the database provider is set in one place in `Program.cs`
4. **Scalability**: Feature-based organization supports team scaling
5. **Reusability**: Core business logic can be reused across different presentation layers

## Building the Solution

```bash
# From the solution root
dotnet build
```

## Testing

The solution has no automated tests yet. The API uses an in-memory database, set in `Program.cs`, so data is reset on each application restart. There is no connection string; using a persistent database means adding an EF Core provider and replacing `UseInMemoryDatabase`.

## Future Enhancements

- Add authentication and authorization
- Implement caching strategies
- Add logging and monitoring
- Implement event sourcing
- Add integration tests
- Configure persistent database
- Add Docker support
