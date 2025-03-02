using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;
using Ambev.DeveloperEvaluation.Application.Products.ListCategories;
using Ambev.DeveloperEvaluation.Application.Products.ListProduct;
using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductResult;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.ORM;
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

        [Fact(DisplayName = "Tests ListProducts action passing no parameter")]
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

        [Fact(DisplayName = "Tests GetProduct action ")]
        public async Task Tests_GetProduct()
        {
            var id = Guid.NewGuid();

            var result = new GetProductResult();

            _mediator.Send(Arg.Any<GetProductCommand>()).Returns(result);

            var response = await _controller.GetProduct(new GetProductCommand { Id = id });

            response.Should().NotBeNull();
            (response as ObjectResult)!.StatusCode.Should().Be(200);
            (response as ObjectResult)!.Value.Should().NotBeNull();
        }

        [Fact(DisplayName = "Tests UpdateProduct")]
        public async Task Tests_UpdateProduct()
        {

            var result = new UpdateProductResult();

            _mediator.Send(Arg.Any<UpdateProductCommand>()).Returns(result);

            var response = await _controller.UpdateProduct(Guid.NewGuid(),new UpdateProductCommand());

            response.Should().NotBeNull();
            (response as ObjectResult)!.StatusCode.Should().Be(201);
            (response as ObjectResult)!.Value.Should().NotBeNull();
            await _mediator.Received(1).Send(Arg.Any<UpdateProductCommand>());
        }

        [Fact(DisplayName = "Tests list categories")]
        public async Task Tests_ListCategories()
        {
            var result = new List<string> { "cat 1", "cat 2", "cat 3" };

            _mediator.Send(Arg.Any<ListCategoriesCommand>()).Returns(result);

            var response = await _controller.ListCategories();

            response.Should().NotBeNull();
            (response as ObjectResult)!.StatusCode.Should().Be(201);
            (response as ObjectResult)!.Value.Should().NotBeNull();
            await _mediator.Received(1).Send(Arg.Any<ListCategoriesCommand>());
        }

        [Fact(DisplayName = "Tests list products by category")]
        public async Task Tests_ListProductsByCategory()
        {
            var result = new ListProductsByCategoryResult();

            _mediator.Send(Arg.Any<ListProductsByCategoryCommand>()).Returns(result);

            var response = await _controller.ListProductsByCategory(new ListProductsByCategoryCommand());

            response.Should().NotBeNull();
            (response as ObjectResult)!.StatusCode.Should().Be(200);
            (response as ObjectResult)!.Value.Should().NotBeNull();
            await _mediator.Received(1).Send(Arg.Any<ListProductsByCategoryCommand>());
        }
    }
}
