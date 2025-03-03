using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Unit.Infrastructure.TestData
{
    public static class ProductRepositoryTestData
    {
        private static readonly Faker<Product> productFaker = new Faker<Product>()
           // Se BaseEntity possui uma propriedade Id, podemos gerá-la
           .RuleFor(p => p.Id, f => f.Random.Guid())
           .RuleFor(p => p.Title, f => f.Commerce.ProductName())
           .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
           .RuleFor(p => p.Description, f => f.Lorem.Sentence(10))
           .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0])
           .RuleFor(p => p.Image, f => f.Image.PicsumUrl())
           .RuleFor(p => p.Rating, f => new ProductRating
           {
               Rate = f.Random.Double(1, 5),
               Count = f.Random.Int(0, 1000)
           })
           // Gera uma data recente para CreatedAt
           .RuleFor(p => p.CreatedAt, f => f.Date.RecentOffset(10).UtcDateTime)
           // Gera uma data recente para UpdatedAt, podendo ser nula se necessário (ajuste conforme seu cenário)
           .RuleFor(p => p.UpdatedAt, f => f.Date.RecentOffset(5).UtcDateTime);

        /// <summary>
        /// Gera uma instância válida da entidade Product com dados aleatórios.
        /// </summary>
        /// <returns>Instância de Product</returns>
        public static Product GenerateValidProduct()
        {
            return productFaker.Generate();
        }
    }
}
