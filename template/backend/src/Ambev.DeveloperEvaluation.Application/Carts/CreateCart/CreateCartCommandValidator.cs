using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartCommandValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty();

            RuleFor(x => x.Products)
                .NotEmpty();

            RuleForEach(x => x.Products).ChildRules(product =>
            {
                product.RuleFor(p => p.ProductId)
                    .NotEqual(Guid.Empty);

                product.RuleFor(p => p.Quantity)
                    .GreaterThan(0);
            });
        }
    }
}
