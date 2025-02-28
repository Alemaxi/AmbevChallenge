using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.ListProduct;
using Ambev.DeveloperEvaluation.Unit.Controller.TestData;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Controller
{
    public class ProductControllerTest
    {
        private readonly IMediator _mediator;
        private readonly ProductsController _controller;

        public ProductControllerTest()
        {
            _mediator = Substitute.For<IMediator>();
            _controller = new ProductsController(_mediator);
        }

        [Fact(DisplayName = "Tests the createProduct action with a valid command -> should work")]
        public async Task Tests_CreateProduct_With_Valid_Command()
        {
            var command = CreateProductControllerTestData.GenerateValidCommand();

            var result = new CreateProductResult
            {
                Category = command.Category,
                Description = command.Description,
                Image = command.Image,
                Price = command.Price,
                Title = command.Title,
                Rating = new DeveloperEvaluation.Application.Products.Shared.ProductRatingCommandShared.ProductRatingResult
                {
                    Count = command.Rating.Count,
                    Rate = command.Rating.Rate,
                }
            };

            _mediator.Send(Arg.Any<CreateProductCommand>()).Returns(result);

            var response = await _controller.CreateProduct(command);

            response.Should().NotBeNull();
            (response as ObjectResult)!.StatusCode.Should().Be(201);
            ((response as ObjectResult)!.Value as ApiResponseWithData<CreateProductResult>)!.Data.Should().BeAssignableTo(typeof(CreateProductResult));
            await _mediator.Received(1).Send(Arg.Any<CreateProductCommand>());
        }

        [Fact(DisplayName = "Tests the createProduct action with an invalid command -> should fail")]
        public async Task Tests_CreateProduct_With_Invalid_Command()
        {
            var command = new CreateProductCommand();


            var exception = await Assert.ThrowsAsync<ValidationException>(() => _controller.CreateProduct(command));

            Assert.Contains("The product title is required", exception.Message);
            Assert.Contains("The product description is required.", exception.Message);
            Assert.Contains("The product category is required.", exception.Message);
            Assert.Contains("The product price must be greater than zero.", exception.Message);
        }

        [Fact(DisplayName = "Tests ListProducts action passing no parameter - will be used default params")]
        public async Task Tests_ListProducts()
        {
            var listProductResult = new ListProductResult
            {
                Page = 1,
                Count = 2,
                Size = 10,
                Produtos = new List<ProductResult> { CreateProductControllerTestData.GenerateProductResult(), CreateProductControllerTestData.GenerateProductResult() }
            };

            _mediator.Send(Arg.Any<ListProductCommand>()).Returns(listProductResult);

            var returned = await _controller.ListProducts(new ListProductCommand());

            returned.Should().NotBeNull();
            (returned as ObjectResult)!.StatusCode.Should().Be(200);
            (returned as ObjectResult)!.Value.Should().NotBeNull();
        }

        [Theory(DisplayName = "Tests ListProducts action passing no parameter - will be used default params")]
        [InlineData(0,0)]
        [InlineData(0,10)]
        [InlineData(1,0)]
        public async Task Tests_ListProducts_With_Invalid_Params(int page, int size)
        {
            var exception = await Assert.ThrowsAsync<ValidationException>( () => _controller.ListProducts(new ListProductCommand { Page = page, Size = size }));

            if(page == 0)
            Assert.Contains("'Page' deve ser superior ou igual a '1'", exception.Message);
            if(size == 0)
            Assert.Contains("'Size' deve ser superior ou igual a '1'", exception.Message);
        }

        [Fact(DisplayName = "Tests GetProductResult action passing a valid Guid ID")]
        public async Task Tests_GetProduct_With_Valid_Id()
        {
            var id = Guid.NewGuid();

            var result = new GetProductResult();

            _mediator.Send(Arg.Any<GetProductCommand>()).Returns(result);

            var response = await _controller.GetProduct(new GetProductCommand { Id = id });

            response.Should().NotBeNull();
            (response as ObjectResult)!.StatusCode.Should().Be(200);
            (response as ObjectResult)!.Value.Should().NotBeNull();
        }

        [Fact(DisplayName = "Tests GetProductResult action passing an invalid Guid ID")]
        public async Task Tests_GetProduct_With_Invalid_Id()
        {
            var id = Guid.Empty;


            var exception = await Assert.ThrowsAsync<ValidationException>(() => _controller.GetProduct(new GetProductCommand { Id = id}));

            Assert.Contains("Invalid Id", exception.Message);
            Assert.Single(exception.Errors);
        }
    }
}
