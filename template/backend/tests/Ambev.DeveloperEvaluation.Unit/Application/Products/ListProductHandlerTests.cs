using Ambev.DeveloperEvaluation.Application.Products.ListProduct;
using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductResult;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class ListProductHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ListProductHandler _handler;

        public ListProductHandlerTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();

            _handler = new ListProductHandler(_unitOfWork, _mapper);
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
            _unitOfWork.Products.GetAllPaginatedAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>()).Returns(listProduct);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            await _unitOfWork.Received(2).Products.GetAllPaginatedAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>());
            _mapper.Received(2).Map<ProductResult>(Arg.Any<Product>());
            result.Produtos.Count.Should().Be(2);
            result.Count.Should().Be(2);
            result.Page.Should().Be(command.Page);
            result.Size.Should().Be(command.Size);
        }

        [Theory(DisplayName = "Tests ListProducts action passing no parameter - will be used default params")]
        [InlineData(0, 0)]
        [InlineData(0, 10)]
        [InlineData(1, 0)]
        public async Task Tests_ListProducts_With_Invalid_Params(int page, int size)
        {
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(new ListProductCommand { Page = page, Size = size },CancellationToken.None));

            if (page == 0)
                Assert.Contains("'Page' deve ser superior ou igual a '1'", exception.Message);
            if (size == 0)
                Assert.Contains("'Size' deve ser superior ou igual a '1'", exception.Message);
        }
    }
}
