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

        public InMemoryDatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            Context = new DefaultContext(options);
            GenerateProducts();
        }

        public void Dispose()
        {
            // Libera os recursos utilizados pelo contexto
            Context.Dispose();
        }


        private void GenerateProducts()
        {
            Context.Products.AddRange(new List<Product>
            {
                ProductRepositoryTestData.GenerateValidProduct(),
                ProductRepositoryTestData.GenerateValidProduct(),
                ProductRepositoryTestData.GenerateValidProduct()
            });

            Context.Carts.AddRange(CartRepositoryTestData.GenerateValidCarts(5));

            Context.SaveChanges();
        }
    }
}
