using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.Shared.ProductRatingCommandShared
{
    public class ProductRatingCommandValidator : AbstractValidator<ProductRatingCommand>
    {
        public ProductRatingCommandValidator()
        {
            RuleFor(x => x.Rate)
            .InclusiveBetween(0.0, 5.0)
            .WithMessage("The product rate must be between 0 and 5.");

            RuleFor(x => x.Count)
                .GreaterThanOrEqualTo(0)
                .WithMessage("The number of ratings must be zero or greater.");
        }
    }
}
