using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductRatingCommandShared;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
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
