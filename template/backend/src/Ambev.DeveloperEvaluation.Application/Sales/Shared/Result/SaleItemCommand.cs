namespace Ambev.DeveloperEvaluation.Application.Sale.Shared.result
{
    public class SaleItemResult
    {
        public int Id { get; set; }

        public Guid SaleId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; private set; }

        public decimal Discount { get; private set; } = 0;

        public decimal TotalAmount { get; set; }
    }
}
