namespace Ambev.DeveloperEvaluation.Application.Carts.Shared.CartResult
{
    public class CartResult
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<CartProductResult> Products { get; set; } = new List<CartProductResult>();
    }
}
