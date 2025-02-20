using Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    public class UpdateProductRequest
    {
        /// <summary>
        /// Título do produto
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Preço do produto
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Descrição do produto
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Categoria do produto
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// URL da imagem do produto
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Informações sobre a avaliação do produto
        /// </summary>
        public ProductRatingDTO Rating { get; set; }
    }
}
