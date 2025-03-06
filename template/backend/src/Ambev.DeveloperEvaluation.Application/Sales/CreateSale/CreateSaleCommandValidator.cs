using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.SaleDate)
                .LessThanOrEqualTo(DateTime.Now);

            RuleFor(x => x.CustomerId)
                .NotEmpty();

            RuleFor(x => x.Branch)
                .NotEmpty();

            RuleFor(x => x.Items)
                .NotEmpty();

            RuleForEach(x => x.Items).ChildRules(items =>
            {
                items.RuleFor(item => item.ProductId)
                     .NotEmpty();

                items.RuleFor(item => item.Quantity)
                     .GreaterThan(0);
            });
        }
    }
}
