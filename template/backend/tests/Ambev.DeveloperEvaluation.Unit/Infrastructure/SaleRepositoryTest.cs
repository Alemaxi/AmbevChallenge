using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.Unit.Infrastructure.Fixture;
using Ambev.DeveloperEvaluation.Unit.Infrastructure.TestData;
using Xunit;
using FluentAssertions;

namespace Ambev.DeveloperEvaluation.Unit.Infrastructure
{
    public class SaleRepositoryTest : IClassFixture<InMemoryDatabaseFixture>
    {
        private readonly InMemoryDatabaseFixture _fixture;
        private readonly DefaultContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public SaleRepositoryTest(InMemoryDatabaseFixture fixture)
        {
            _fixture = fixture;
            _context = fixture.Context;

            _unitOfWork = new UnitOfWork(_context);
            fixture.GenerateSales();
        }

        [Fact(DisplayName = "testing getAll Method")]
        public async Task GetAllTest()
        {
            var total = _context.Sales.Count();

            var result = await _unitOfWork.Sales.GetAllAsync();

            result.Count().Should().Be(total);
        }

        [Fact(DisplayName = "testing GetAllPaged for a serch with data")]
        public async Task GetAllPaged()
        {
            var result = await _unitOfWork.Sales.GetAllPaginatedAsync(1, 10, "");

            result.Should().NotBeNull();
            result.Should().HaveCountLessThanOrEqualTo(10);
        }

        [Fact(DisplayName = "testing GetAllPaged for a serch with no data using page")]
        public async Task GetAllPagedWithNoDataByPage()
        {
            var result = await _unitOfWork.Sales.GetAllPaginatedAsync(10, 10, "");

            result.Should().NotBeNull();
            result.Should().HaveCountLessThanOrEqualTo(10);
            result.Should().HaveCount(0);
        }

        [Fact(DisplayName = "testing GetAllPaged for a serch with no data using size")]
        public async Task GetAllPagedWithNoDataBySize()
        {
            var result = await _unitOfWork.Sales.GetAllPaginatedAsync(1, 0, "");

            result.Should().NotBeNull();
            result.Should().HaveCountLessThanOrEqualTo(0);
            result.Should().HaveCount(0);
        }

        [Fact(DisplayName = "testing GetAllPaged Ordered")]
        public async Task GetAllPagedWithOrdering()
        {
            var result = await _unitOfWork.Sales.GetAllPaginatedAsync(1, 10, "SaleDate Desc");

            result.Should().NotBeNull();
            result.Should().HaveCountLessThanOrEqualTo(10);
            result.First().SaleDate.Should().BeAfter(result.Last().SaleDate);
        }


        [Fact(DisplayName = "testing Getting a Sale by id")]
        public async Task GetSaleById()
        {
            var sale = _context.Sales.FirstOrDefault();

            var result = await _unitOfWork.Sales.GetByIdAsync(sale.Id);

            result.Should().NotBeNull();
            result.Should().Be(sale);
        }

        [Fact(DisplayName = "testing Creating a Sale")]
        public async Task CreateASale()
        {
            var sale = SaleRepositoryTestData.GenerateValidSale();

            var result = await _unitOfWork.Sales.CreateAsync(sale);
            await _unitOfWork.CommitAsync();

            result.Should().NotBeNull();
            _context.Sales.FirstOrDefault(s => sale.Id == s.Id).Should().NotBeNull();

            _context.Remove(sale);
            _context.SaveChanges();
        }


        [Fact(DisplayName = "testing Updating a Sale")]
        public async Task UpdateASale()
        {
            var sale = _context.Sales.FirstOrDefault();

            var oldDate = sale.SaleDate;

            sale.SaleDate = sale.SaleDate.AddDays(2);

            var result = await _unitOfWork.Sales.UpdateAsync(sale);
            await _unitOfWork.CommitAsync();

            result.Should().NotBeNull();
            _context.Sales.FirstOrDefault(s => sale.Id == s.Id)!.SaleDate.Should().NotBe(oldDate);

        }


        [Fact(DisplayName = "testing Deleting a Sale")]
        public async Task DeleteACart()
        {
            var sale = _context.Sales.FirstOrDefault();

            var result = await _unitOfWork.Sales.DeleteAsync(sale.Id);
            await _unitOfWork.CommitAsync();

            _context.Sales.Add(sale);
            _context.SaveChanges();
        }
    }
}
