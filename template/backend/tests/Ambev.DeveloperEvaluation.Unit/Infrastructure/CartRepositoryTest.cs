using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Unit.Infrastructure.Fixture;
using Ambev.DeveloperEvaluation.Unit.Infrastructure.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Infrastructure
{
    public class CartRepositoryTest : IClassFixture<InMemoryDatabaseFixture>
    {
        private readonly InMemoryDatabaseFixture _fixture;
        private readonly DefaultContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public CartRepositoryTest(InMemoryDatabaseFixture fixture)
        {
            _fixture = fixture;
            _context = fixture.Context;

            _unitOfWork = new UnitOfWork(_context);
        }

        [Fact(DisplayName = "testing getAll Method")]
        public async Task GetAllTest()
        {
            var total = _context.Carts.Count();

            var result = await _unitOfWork.Carts.GetAllAsync();

            result.Count().Should().Be(total);
        }

        [Fact(DisplayName = "testing GetAllPaged for a serch with data")]
        public async Task GetAllPaged()
        {
            var result = await _unitOfWork.Carts.GetAllPaginatedAsync(1, 10, "");

            result.Should().NotBeNull();
            result.Should().HaveCountLessThanOrEqualTo(10);
        }

        [Fact(DisplayName = "testing GetAllPaged for a serch with no data using page")]
        public async Task GetAllPagedWithNoDataByPage()
        {
            var result = await _unitOfWork.Carts.GetAllPaginatedAsync(10, 10, "");

            result.Should().NotBeNull();
            result.Should().HaveCountLessThanOrEqualTo(10);
            result.Should().HaveCount(0);
        }

        [Fact(DisplayName = "testing GetAllPaged for a serch with no data using size")]
        public async Task GetAllPagedWithNoDataBySize()
        {
            var result = await _unitOfWork.Carts.GetAllPaginatedAsync(1, 0, "");

            result.Should().NotBeNull();
            result.Should().HaveCountLessThanOrEqualTo(0);
            result.Should().HaveCount(0);
        }

        [Fact(DisplayName = "testing GetAllPaged Ordered")]
        public async Task GetAllPagedWithOrdering()
        {
            var result = await _unitOfWork.Carts.GetAllPaginatedAsync(1, 10, "Date Desc");

            result.Should().NotBeNull();
            result.Should().HaveCountLessThanOrEqualTo(10);
            result.Should().HaveCount(5);
            result.First().Date.Should().BeAfter(result.Last().Date);
        }


        [Fact(DisplayName = "testing Getting a card by id")]
        public async Task GetCartById()
        {
            var cart = _context.Carts.FirstOrDefault();

            var result = await _unitOfWork.Carts.GetByIdAsync(cart.Id);

            result.Should().NotBeNull();
            result.Should().Be(cart);
        }

        [Fact(DisplayName = "testing Creating a card")]
        public async Task CreateACart()
        {
            var cart = CartRepositoryTestData.GenerateValidCart();

            var result = await _unitOfWork.Carts.CreateAsync(cart);
            await _unitOfWork.CommitAsync();

            result.Should().NotBeNull();
            _context.Carts.FirstOrDefault(c => cart.Id == c.Id).Should().NotBeNull();

            _context.Remove(cart);
            _context.SaveChanges();
        }


        [Fact(DisplayName = "testing Updating a card")]
        public async Task UpdateACart()
        {
            var cart = _context.Carts.FirstOrDefault();

            var oldDate = cart.Date;

            cart.Date = cart.Date.AddDays(2);

            var result = await _unitOfWork.Carts.UpdateAsync(cart);
            await _unitOfWork.CommitAsync();

            result.Should().NotBeNull();
            _context.Carts.FirstOrDefault(c => cart.Id == c.Id)!.Date.Should().NotBe(oldDate);

        }


        [Fact(DisplayName = "testing Deleting a card")]
        public async Task DeleteACart()
        {
            var cart = _context.Carts.FirstOrDefault();

            var result = await _unitOfWork.Carts.DeleteAsync(cart.Id);
            await _unitOfWork.CommitAsync();

            _context.Carts.Add(cart);
            _context.SaveChanges();
        }
    }
}
