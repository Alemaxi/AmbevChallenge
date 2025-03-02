using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;
using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductResult;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class ListProductsByCategoryHandlerTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ListProductsByCategoryHandler _handler;
        public ListProductsByCategoryHandlerTest()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();

            _handler = new ListProductsByCategoryHandler(_unitOfWork, _mapper);
        }


        [Fact(DisplayName = "It tests Getting products by category")]
        public async Task TestsListProductsByCategory()
        {
            var command = new ListProductsByCategoryCommand();
            command.Category = "cat 1";

            var products = new List<Product> { new Product(), new Product() };

            _unitOfWork.Products.ListProductsByCategory(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>()).Returns(products);
            _mapper.Map<ProductResult>(Arg.Any<Product>()).Returns(new ProductResult());

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Count.Should().Be(2);
            result.Produtos.Count.Should().Be(2);
            result.Page.Should().Be(command.Page);
            result.Size.Should().Be(command.Size);
        }


        [Fact(DisplayName = "It tests Getting products by category fail")]
        public async Task TestsListProductsByCategoryFail()
        {
            var command = new ListProductsByCategoryCommand { Category = string.Empty, Page = 0, Size = 0 };

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            exception.Should().NotBeNull();
            exception.Errors.Count().Should().Be(3);
        }
    }
}
