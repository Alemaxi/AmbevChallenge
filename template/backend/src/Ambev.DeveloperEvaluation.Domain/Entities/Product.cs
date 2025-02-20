using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Product : BaseEntity
    {
        /// <summary>
        /// Título do produto.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Preço do produto.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Descrição do produto.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Categoria do produto.
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// URL da imagem do produto.
        /// </summary>
        public string Image { get; set; } = string.Empty;

        /// <summary>
        /// Informações sobre a avaliação do produto.
        /// </summary>
        public ProductRating Rating { get; set; } = new ProductRating();

        /// <summary>
        /// Data de criação do registro.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Data da última atualização do registro.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
