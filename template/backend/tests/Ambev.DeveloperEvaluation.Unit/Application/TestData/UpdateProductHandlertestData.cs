﻿using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductRatingCommandShared;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public class UpdateProductHandlertestData
    {
        private static readonly Faker<UpdateProductCommand> updateProductHandlerFaker = new Faker<UpdateProductCommand>()
        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000)) // Preço aleatório entre 1 e 1000
        .RuleFor(p => p.Description, f => f.Lorem.Sentence(10)) // Descrição curta aleatória
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0]) // Categoria de produto aleatória
        .RuleFor(p => p.Image, f => f.Image.PicsumUrl()) // URL de imagem fictícia
        .RuleFor(p => p.Rating, f => new ProductRatingCommand
        {
            Rate = f.Random.Double(1, 5), // Avaliação de 1 a 5 estrelas
            Count = f.Random.Int(0, 1000) // Número de avaliações
        });

        /// <summary>
        /// Generates a valid Product entity with randomized data.
        /// The generated Product will have all properties populated with valid values
        /// that meet the system's validation requirements.
        /// </summary>
        /// <returns>A valid Product entity with randomly generated data.</returns>
        public static UpdateProductCommand GenerateValidCommand()
        {
            return updateProductHandlerFaker.Generate();
        }
    }
}
