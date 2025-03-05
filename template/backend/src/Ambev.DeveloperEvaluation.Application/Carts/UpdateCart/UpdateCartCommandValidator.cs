using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

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
