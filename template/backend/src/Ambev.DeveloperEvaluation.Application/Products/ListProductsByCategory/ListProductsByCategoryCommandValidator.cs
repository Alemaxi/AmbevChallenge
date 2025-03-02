using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory
{
    public class ListProductsByCategoryCommandValidator : AbstractValidator<ListProductsByCategoryCommand>
    {
        public ListProductsByCategoryCommandValidator()
        {
            RuleFor(c => c.Page).GreaterThanOrEqualTo(1);
            RuleFor(c => c.Size).GreaterThanOrEqualTo(1);
            RuleFor(c => c.Category).NotEmpty();
        }
    }
}
