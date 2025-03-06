using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSalesPaged
{
    public class ListSalesPagedCommandValidator : AbstractValidator<ListSalesPagedCommand>
    {
        public ListSalesPagedCommandValidator() 
        {
            RuleFor(c => c.Page).GreaterThanOrEqualTo(1);
            RuleFor(c => c.Size).GreaterThanOrEqualTo(1);
        }
    }
}
