using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductResult;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory
{
    public class ListProductsByCategoryResult
    {
        public List<ProductResult> Produtos { get; set; } = new List<ProductResult>();
        public int Page { get; set; }
        public int Size { get; set; }
        public int Count { get; set; }
    }
}
