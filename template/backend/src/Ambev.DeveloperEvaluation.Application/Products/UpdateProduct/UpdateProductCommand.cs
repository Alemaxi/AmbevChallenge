using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductRatingCommandShared;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductCommand : IRequest<UpdateProductResult>
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
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Categoria do produto
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// URL da imagem do produto
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Informações sobre a avaliação do produto
        /// </summary>
        public ProductRatingCommand Rating { get; set; } = new ProductRatingCommand();
    }
}
