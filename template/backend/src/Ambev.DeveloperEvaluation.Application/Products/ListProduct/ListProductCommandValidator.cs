using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct
{
    public class ListProductCommandValidator : AbstractValidator<ListProductCommand>
    {
        public ListProductCommandValidator()
        {
            RuleFor(c => c.Page).GreaterThanOrEqualTo(1);
            RuleFor(c => c.Size).GreaterThanOrEqualTo(1);
        }
    }
}
