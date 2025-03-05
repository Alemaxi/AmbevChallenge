using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public int Id { get; set; }

        /// <summary>
        /// Chave estrangeira para a venda a qual o item pertence.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Venda a qual o item pertence.
        /// </summary>
        public Sale Sale { get; set; }

        /// <summary>
        /// Chave estrangeira para o produto vendido.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Produto vendido.
        /// Aqui utilizamos a classe Product para referenciar o produto.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Quantidade do produto vendido.
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Desconto aplicado ao item.
        /// </summary>
        public decimal Discount { get; private set; } = 0;

        public Decimal GetTotalAmount()
        {
            var totalWithNoDiscount = Quantity * Product.Price;
            var discountToBeApplied = Discount / 100;

            return totalWithNoDiscount - (totalWithNoDiscount * discountToBeApplied);
        }

        public void SetQuantity(int quantity)
        {
            Quantity = quantity;

            if (quantity > 10) { Discount = 20; return; }
            if (quantity > 4) { Discount = 10; return; }
        }
    }
}
