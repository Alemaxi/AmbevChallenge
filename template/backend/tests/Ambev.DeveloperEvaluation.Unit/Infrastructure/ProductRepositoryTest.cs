using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Unit.Infrastructure.Fixture;
using Ambev.DeveloperEvaluation.Unit.Infrastructure.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Infrastructure
{
    public class ProductRepositoryTest : IClassFixture<InMemoryDatabaseFixture>
    {
        private readonly InMemoryDatabaseFixture _fixture;
        private readonly IUnitOfWork _unitOfWork;
        private ProductRepository _repository;
        public DefaultContext Context { get; set; }

        public ProductRepositoryTest(InMemoryDatabaseFixture fixture)
        {
            _fixture = fixture;
            Context = fixture.Context;
            _unitOfWork = new UnitOfWork(Context);
            _repository = new ProductRepository(Context);

            fixture.GenerateProducts();
        }

        [Fact(DisplayName = "Tests Get all Products")]
        public async Task ListAllProductsTest()
        {
            var total = Context.Products.Count();

            var result = await _repository.GetAllAsync();

            result.Should().NotBeNull();
            result.Count().Should().Be(total);
        }

        [Fact(DisplayName = "Tests List Product")]
        public async Task ListProductsTest()
        {
            var result = await _repository.GetAllPaginatedAsync(1,10, "", CancellationToken.None);

            result.Should().NotBeNull();
            result.Count().Should().BeLessThanOrEqualTo(10);
        }

        [Fact(DisplayName = "Tests List Product for page with no data")]
        public async Task ListProductsTestForPage()
        {

            var result = await _repository.GetAllPaginatedAsync(10, 10, "", CancellationToken.None);

            result.Should().NotBeNull();
            result.Count().Should().Be(0);
        }

        [Fact(DisplayName = "Tests List Product for a diferent size")]
        public async Task ListProductsTestForDiferentSize()
        {

            var result = await _repository.GetAllPaginatedAsync(1, 2, "", CancellationToken.None);

            result.Should().NotBeNull();
            result.Count().Should().Be(2);
        }

        [Fact(DisplayName = "Tests List Product for a 0 size")]
        public async Task ListProductsTestForZeroSize()
        {

            var result = await _repository.GetAllPaginatedAsync(1, 0, "", CancellationToken.None);

            result.Should().NotBeNull();
            result.Count().Should().Be(0);
        }

        [Fact(DisplayName = "Tests Ordering Product using Price")]
        public async Task ListProductsTestOrderingByPrice()
        {

            var result = await _repository.GetAllPaginatedAsync(1, 10, "Price Desc", CancellationToken.None);

            result.Should().NotBeNull();
            result.Count().Should().Be(3);
            var comparing = result.First().Price > result.Last().Price;

            comparing.Should().BeTrue();
        }

        [Fact(DisplayName = "Tests Creating a product")]
        public async Task CreateProductsTest()
        {
            var product = ProductRepositoryTestData.GenerateValidProduct();

            var result = await  _unitOfWork.Products.CreateAsync(product);
            await _unitOfWork.CommitAsync();

            Context.Products.FirstOrDefault(p => p.Id == product.Id).Should().NotBeNull();
            Context.Products.Remove(product);
        }

        [Fact(DisplayName = "Tests Updating a product")]
        public async Task UpdateProductsTest()
        {
            var product = ProductRepositoryTestData.GenerateValidProduct();

            await _unitOfWork.Products.CreateAsync(product);
            await _unitOfWork.CommitAsync();

            var oldPrice = product.Price;

            product.Price = product.Price + 1;

            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.CommitAsync();

            Context.Products.FirstOrDefault(p => p.Id == product.Id).Price.Should().Be(oldPrice + 1);
            Context.Products.Remove(product);
        }

        [Fact(DisplayName = "Tests Deleting a product")]
        public async Task DeleteProductsTest()
        {
            var product = ProductRepositoryTestData.GenerateValidProduct();

            await _unitOfWork.Products.CreateAsync(product);
            await _unitOfWork.CommitAsync();

            Context.Products.FirstOrDefault( p  => p.Id == product.Id).Should().NotBeNull();

            await _unitOfWork.Products.DeleteAsync(product.Id);
            await _unitOfWork.CommitAsync();

            Context.Products.FirstOrDefault(p => p.Id == product.Id).Should().BeNull();
        }

        [Fact(DisplayName = "Tests getting all categories")]
        public async Task ListCategoriesTest()
        {
            var categories = Context.Products.Select(p => p.Category).Distinct().Count();

            var gottenCategories = await _unitOfWork.Products.ListCategories();

            gottenCategories.Count().Should().Be(categories);
        }

        [Fact(DisplayName = "Tests getting products for a category")]
        public async Task ListProductsByCategory()
        {
            var category = "cat";
            var product1 = ProductRepositoryTestData.GenerateValidProduct();
            product1.Category = category;
            var product2 = ProductRepositoryTestData.GenerateValidProduct();
            product2.Category = category;

            Context.Products.AddRange(new List<Product> { product1, product2 });
            Context.SaveChanges();

            var products = await _unitOfWork.Products.ListProductsByCategory(category,1, 10,"");

            products.Count().Should().Be(2);

            Context.Products.RemoveRange(new List<Product> { product1, product2 });
            Context.SaveChanges();
        }

    }
}
