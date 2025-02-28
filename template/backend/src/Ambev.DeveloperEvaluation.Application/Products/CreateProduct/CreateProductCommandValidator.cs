using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductRatingCommandShared;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Title)
        .NotEmpty().WithMessage("The product title is required.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("The product description is required.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("The product category is required.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("The product price must be greater than zero.");

            RuleFor(x => x.Image)
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.Image))
                .WithMessage("The product image must be a valid URL.");

            RuleFor(x => x.Rating)
                .SetValidator(new ProductRatingCommandValidator());
        }
    }
}
