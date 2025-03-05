using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Unit.Infrastructure.TestData;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Unit.Infrastructure.Fixture
{
    public class InMemoryDatabaseFixture : IDisposable
    {
        public DefaultContext Context { get; private set; }
        private bool _seeded = false;

        public InMemoryDatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            Context = new DefaultContext(options);
        }

        public void Dispose()
        {
            // Libera os recursos utilizados pelo contexto
            Context.Dispose();
        }


        public void GenerateProducts()
        {
            Context.Products.AddRange(new List<Product>
            {
                ProductRepositoryTestData.GenerateValidProduct(),
                ProductRepositoryTestData.GenerateValidProduct(),
                ProductRepositoryTestData.GenerateValidProduct()
            });

            Context.SaveChanges();
            _seeded = true;
        }
        public void GenerateCarts()
        {
            Context.Carts.AddRange(CartRepositoryTestData.GenerateValidCarts(5));

            Context.SaveChanges();
            _seeded = true;
        }

        public void GenerateSales()
        {
            Context.Sales.AddRange(SaleRepositoryTestData.GenerateValidSales(5));

            Context.SaveChanges();
            _seeded = true;
        }
    }
}
