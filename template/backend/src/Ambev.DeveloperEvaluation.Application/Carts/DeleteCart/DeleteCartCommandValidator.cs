using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
    public class DeleteCartCommandValidator : AbstractValidator<DeleteCartCommand>
    {
        public DeleteCartCommandValidator()
        {
            RuleFor(x => x.Id).Must(guid => guid != Guid.Empty)
               .WithMessage("Invalid Id");
        }
    }
}
