using Ambev.DeveloperEvaluation.Application.Products.ListProduct;
using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductRatingCommandShared;
using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductResult;
using Bogus;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class ListProductHandlerTestData
    {
        // Primeiro definimos um Faker para gerar ProductResult
        private static readonly Faker<ProductResult> productResultFaker = new Faker<ProductResult>("pt_BR")
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Title, f => f.Commerce.ProductName())
            .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(1, 1000)))
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0]) // Ex: retorna uma categoria
            .RuleFor(p => p.Image, f => f.Image.PicsumUrl())             // Ex: gera uma URL de imagem
            .RuleFor(p => p.Rating, f => new ProductRatingResult
            {
                Rate = f.Random.Double(1, 5),
                Count = f.Random.Int(1, 1000)
            });

        // Em seguida, criamos o Faker para ListProductResult
        public static readonly Faker<ListProductResult> listProductResultFaker = new Faker<ListProductResult>("pt_BR")
            .RuleFor(l => l.Produtos, f => productResultFaker.Generate(f.Random.Int(5, 15)))
            .RuleFor(l => l.Page, f => f.Random.Int(1, 5))
            .RuleFor(l => l.Size, f => f.Random.Int(5, 15))
            .RuleFor(l => l.Count, f => f.Random.Int(50, 500));

        public static ListProductResult GenerateResult()
        {
            return listProductResultFaker.Generate();
        }
    }

}