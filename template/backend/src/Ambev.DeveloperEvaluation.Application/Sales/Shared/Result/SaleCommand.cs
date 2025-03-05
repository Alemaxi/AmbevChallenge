using Ambev.DeveloperEvaluation.Application.Sale.Shared.result;

namespace Ambev.DeveloperEvaluation.Application.Sale.Shared.Result
{
    public class SaleResult
    {
        public DateTime SaleDate { get; set; }

        public Guid CustomerId { gt; set; }

        public string Branch { get; set; } = string.Empty;

        public List<SaleItemResult> Items { get; set; } = new List<SaleItemResult>();

        public decimal TotalAmount { get; set; }

        public bool IsCancelled { get; set; }
    }
}
