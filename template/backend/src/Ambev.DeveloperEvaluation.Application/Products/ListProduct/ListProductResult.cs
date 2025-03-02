using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductResult;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct
{
    public class ListProductResult
    {
        public List<ProductResult> Produtos { get; set; } = new List<ProductResult>();
        public int Page { get; set; }
        public int Size { get; set; }
        public int Count { get; set; }
    }
}
