using FluentValidation;
using ProductCatalog.Application.Features.Products.Commands;

namespace ProductCatalog.Application.Features.Products.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Product.Name)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(200).WithMessage("Product name cannot exceed 200 characters");

        RuleFor(x => x.Product.Description)
            .NotEmpty().WithMessage("Product description is required")
            .MaximumLength(2000).WithMessage("Product description cannot exceed 2000 characters");

        RuleFor(x => x.Product.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than zero");

        RuleFor(x => x.Product.Currency)
            .NotEmpty().WithMessage("Currency is required")
            .Length(3).WithMessage("Currency must be 3 characters long");

        RuleFor(x => x.Product.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative");

        RuleFor(x => x.Product.Category)
            .NotEmpty().WithMessage("Product category is required")
            .MaximumLength(100).WithMessage("Product category cannot exceed 100 characters");

        RuleFor(x => x.Product.Brand)
            .MaximumLength(100).WithMessage("Brand name cannot exceed 100 characters")
            .When(x => !string.IsNullOrEmpty(x.Product.Brand));

        RuleFor(x => x.Product.Sku)
            .MaximumLength(50).WithMessage("SKU cannot exceed 50 characters")
            .When(x => !string.IsNullOrEmpty(x.Product.Sku));

        RuleFor(x => x.Product.Weight)
            .GreaterThan(0).WithMessage("Weight must be greater than zero")
            .When(x => x.Product.Weight.HasValue);

        RuleFor(x => x.Product.ImageUrl)
            .MaximumLength(500).WithMessage("Image URL cannot exceed 500 characters")
            .When(x => !string.IsNullOrEmpty(x.Product.ImageUrl));
    }
}

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Product ID is required");

        RuleFor(x => x.Product.Name)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(200).WithMessage("Product name cannot exceed 200 characters");

        RuleFor(x => x.Product.Description)
            .NotEmpty().WithMessage("Product description is required")
            .MaximumLength(2000).WithMessage("Product description cannot exceed 2000 characters");

        RuleFor(x => x.Product.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than zero");

        RuleFor(x => x.Product.Currency)
            .NotEmpty().WithMessage("Currency is required")
            .Length(3).WithMessage("Currency must be 3 characters long");

        RuleFor(x => x.Product.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative");

        RuleFor(x => x.Product.Category)
            .NotEmpty().WithMessage("Product category is required")
            .MaximumLength(100).WithMessage("Product category cannot exceed 100 characters");

        RuleFor(x => x.Product.Brand)
            .MaximumLength(100).WithMessage("Brand name cannot exceed 100 characters")
            .When(x => !string.IsNullOrEmpty(x.Product.Brand));

        RuleFor(x => x.Product.Sku)
            .MaximumLength(50).WithMessage("SKU cannot exceed 50 characters")
            .When(x => !string.IsNullOrEmpty(x.Product.Sku));

        RuleFor(x => x.Product.Weight)
            .GreaterThan(0).WithMessage("Weight must be greater than zero")
            .When(x => x.Product.Weight.HasValue);

        RuleFor(x => x.Product.ImageUrl)
            .MaximumLength(500).WithMessage("Image URL cannot exceed 500 characters")
            .When(x => !string.IsNullOrEmpty(x.Product.ImageUrl));
    }
}
