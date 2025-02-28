using Ambev.DeveloperEvaluation.Application.Products.ListProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class ListProductHandlerTests
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ListProductHandler _handler;

        public ListProductHandlerTests()
        {
            _repository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();

            _handler = new ListProductHandler(_repository, _mapper);
        }


        [Fact(DisplayName = "Tests ListProducts. it returns a list of products")]
        public async Task Tests_listProducts()
        {

            var listProduct = new List<Product> { new Product(), new Product() };

            var command = new ListProductCommand
            {
                Order = "",
                Page = 1,
                Size = 10
            };


            _mapper.Map<ProductResult>(Arg.Any<Product>()).Returns(new ProductResult());
            _repository.GetAllPaginated(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>()).Returns(listProduct);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            _repository.Received(1).GetAllPaginated(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>());
            _mapper.Received(2).Map<ProductResult>(Arg.Any<Product>());
            result.Produtos.Count.Should().Be(2);
            result.Count.Should().Be(2);
            result.Page.Should().Be(command.Page);
            result.Size.Should().Be(command.Size);
        }
    }
}
