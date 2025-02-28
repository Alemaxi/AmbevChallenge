using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductRatingCommandShared;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductResult
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Image { get; set; }

        public ProductRatingResult Rating { get; set; } = new ProductRatingResult();
    }
}
