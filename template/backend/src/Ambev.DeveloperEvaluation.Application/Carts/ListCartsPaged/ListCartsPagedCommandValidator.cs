using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCartsPaged
{
    public class ListCartsPagedCommandValidator : AbstractValidator<ListCartsPagedCommand>
    {
        public ListCartsPagedCommandValidator()
        {
            RuleFor(c => c.Page).GreaterThanOrEqualTo(1);
            RuleFor(c => c.Size).GreaterThanOrEqualTo(1);
        }
    }
}
