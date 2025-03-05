using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale: BaseEntity
    {
        /// <summary>
        /// Date when the sale were made
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// User foreign key
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Navigational reference to the user
        /// </summary>
        public User Customer { get; set; }

        /// <summary>
        /// Filial onde a venda foi realizada.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Lista de itens vendidos.
        /// </summary>
        public List<SaleItem> Items { get; set; } = new List<SaleItem>();

        /// <summary>
        /// Indica se a venda foi cancelada (true) ou não (false).
        /// </summary>
        public bool IsCancelled { get; set; }

        public Decimal GetTotalSaleAmount()
        {
            return Items.Sum(si => si.GetTotalAmount());
        }
    }
}
