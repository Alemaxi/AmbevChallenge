﻿using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductRatingCommandShared;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductCommand : IRequest<UpdateProductResult>
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Product title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Product descritption
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Product category
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Prouduct image Url
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Product Rating informations
        /// </summary>
        public ProductRatingCommand Rating { get; set; } = new ProductRatingCommand();
    }
}
