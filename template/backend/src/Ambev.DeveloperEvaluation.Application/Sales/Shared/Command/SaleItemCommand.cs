namespace Ambev.DeveloperEvaluation.Application.Sales.Shared.Command
{
    public class SaleItemCommand
    {
        public int Id { get; set; }
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
