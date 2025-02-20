namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared
{
    public class ProductRatingDTO
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
