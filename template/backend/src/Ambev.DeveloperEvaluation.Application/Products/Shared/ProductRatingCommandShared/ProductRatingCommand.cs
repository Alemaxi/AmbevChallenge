namespace Ambev.DeveloperEvaluation.Application.Products.Shared.ProductRatingCommandShared
{
    public class ProductRatingCommand
    {
        /// <summary>
        /// Valor da avaliação do produto
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// Número de avaliações
        /// </summary>
        public int Count { get; set; }
    }
}
