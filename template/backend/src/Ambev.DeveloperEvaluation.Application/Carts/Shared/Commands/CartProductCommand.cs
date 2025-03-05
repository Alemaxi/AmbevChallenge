namespace Ambev.DeveloperEvaluation.Application.Carts.Shared.Commands
{
    public class CartProductCommand
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
