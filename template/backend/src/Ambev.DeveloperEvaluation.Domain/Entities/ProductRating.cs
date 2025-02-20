using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class ProductRating
    {
        /// <summary>
        /// Valor da avaliação do produto.
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// Quantidade de avaliações.
        /// </summary>
        public int Count { get; set; }
    }
}
