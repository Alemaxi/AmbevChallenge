using Ambev.DeveloperEvaluation.Application.Sales.Shared.result;

namespace Ambev.DeveloperEvaluation.Application.Sales.Shared.Result
{
    public class SaleResult
    {
        public Guid Id { get; set; }
        public DateTime SaleDate { get; set; }

        public Guid CustomerId { get; set; }

        public string Branch { get; set; } = string.Empty;

        public List<SaleItemResult> Items { get; set; } = new List<SaleItemResult>();

        public decimal TotalAmount { get; set; }

        public bool IsCancelled { get; set; }
    }
}
