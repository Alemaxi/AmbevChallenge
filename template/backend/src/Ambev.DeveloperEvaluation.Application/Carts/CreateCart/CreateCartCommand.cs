using Ambev.DeveloperEvaluation.Application.Carts.Shared.Commands;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartCommand : IRequest<CreateCartResult>
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<CartProductCommand> Products { get; set; } = new List<CartProductCommand>();
    }
}
