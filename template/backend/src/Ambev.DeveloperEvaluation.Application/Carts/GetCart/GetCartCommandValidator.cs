using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    public class GetCartCommandValidator : AbstractValidator<GetCartCommand>
    {
        public GetCartCommandValidator()
        {
            RuleFor(x => x.Id).Must(guid => guid != Guid.Empty)
                .WithMessage("Invalid Id");
        }
    }
}
