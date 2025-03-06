using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductRatingCommandShared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductCommand : IRequest<UpdateProductResult>
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public Guid Id { get; private set; }
        
        [FromBody]
        /// <summary>
        /// Product title
        /// </summary>
        public string Title { get; set; }

        [FromBody]
        /// <summary>
        /// Product price
        /// </summary>
        public decimal Price { get; set; }

        [FromBody]
        /// <summary>
        /// Product descritption
        /// </summary>
        public string Description { get; set; } = string.Empty;

        [FromBody]
        /// <summary>
        /// Product category
        /// </summary>
        public string Category { get; set; } = string.Empty;

        [FromBody]
        /// <summary>
        /// Prouduct image Url
        /// </summary>
        public string Image { get; set; }

        [FromBody]
        /// <summary>
        /// Product Rating informations
        /// </summary>
        public ProductRatingCommand Rating { get; set; } = new ProductRatingCommand();


        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
