using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Infrastructure.TestData
{
    public class SaleRepositoryTestData
    {

        private static readonly Faker<Sale> saleFaker = new Faker<Sale>()
        .RuleFor(s => s.Id, f => Guid.NewGuid())
        .RuleFor(s => s.SaleDate, f => f.Date.Past())
        .RuleFor(s => s.CustomerId, f => Guid.NewGuid())
        .RuleFor(s => s.Customer, f => new User())
        .RuleFor(s => s.Branch, f => f.Company.CompanyName())
        .RuleFor(s => s.IsCancelled, f => f.Random.Bool())
        .RuleFor(s => s.Items, f => new List<SaleItem> { new SaleItem(), new SaleItem()});

        /// <summary>
        /// Gera uma instância válida da entidade Sale com dados aleatórios.
        /// </summary>
        /// <returns>Instância da entidade Sale</returns>
        public static Sale GenerateValidSale()
        {
            return saleFaker.Generate();
        }

        /// <summary>
        /// Gera uma coleção de instâncias válidas da entidade Sale com dados aleatórios.
        /// </summary>
        /// <param name="count">Número de vendas a serem geradas</param>
        /// <returns>Lista de instâncias da entidade Sale</returns>
        public static IEnumerable<Sale> GenerateValidSales(int count = 10)
        {
            return saleFaker.Generate(count);
        }

        /// <summary>
        /// Gera uma instância válida da entidade SaleItem com dados aleatórios.
        /// Os objetos relacionados (Product) são instanciados diretamente com new.
        /// </summary>
        /// <returns>Instância da entidade SaleItem</returns>
        public static SaleItem GenerateValidSaleItem()
        {
            var f = new Faker();
            var product = new Product
            {
                Id =Guid.NewGuid(),
                Title = f.Commerce.ProductName(),
                Price = f.Random.Decimal(10, 1000),
                Description = f.Lorem.Sentence(10),
                Category = f.Commerce.Categories(1)[0],
                Image = f.Image.PicsumUrl(),
                CreatedAt = f.Date.Past(),
                UpdatedAt = f.Date.Recent()
            };

            // Cria o item da venda
            var saleItem = new SaleItem
            {
                Id = f.Random.Int(1, 1000),
                ProductId = product.Id,
                Product = product
            };

            int quantity = f.Random.Int(1, 20);
            saleItem.SetQuantity(quantity);

            return saleItem;
        }
    }
}
