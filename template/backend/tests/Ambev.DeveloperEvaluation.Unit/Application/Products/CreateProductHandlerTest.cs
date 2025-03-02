using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class CreateProductHandlerTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CreateProductHandler _handler;
        private readonly IMapper _mapper;

        public CreateProductHandlerTest()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateProductHandler(_unitOfWork, _mapper);
        }

        // It tests if the all calls needed to create a new product will be made. That test assumes that's a valid command since the validation happens on controller.
        [Fact(DisplayName = "Create Product Successfully")]
        public async Task Handle_WithValidCommand_ShouldCreateProduct()
        {
            var command = CreateProductHandlerTestData.GenerateValidCommand();

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Category = command.Category,
                CreatedAt = DateTime.UtcNow,
                Image = command.Image,
                Description = command.Description,
                Price = command.Price,
                Title = command.Title,
                UpdatedAt = DateTime.UtcNow,
                Rating = new ProductRating
                {
                    Id = Guid.NewGuid(),
                    Count = command.Rating.Count,
                    Rate = command.Rating.Rate
                }
            };

            var result = new CreateProductResult
            {
                Category = command.Category,
                Description = command.Description,
                Price = command.Price,
                Title = command.Title,
                Image = command.Image,
                Rating = new DeveloperEvaluation.Application.Products.Shared.ProductRatingCommandShared.ProductRatingResult
                {
                    Count = command.Rating.Count,
                    Rate = command.Rating.Rate
                }
            };

            _mapper.Map<Product>(command).Returns(product);
            _mapper.Map<CreateProductResult>(product).Returns(result);

            _unitOfWork.Products.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
                .Returns(product);

            // Act
            var createResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            createResult.Should().NotBeNull();
            createResult.Category.Should().Be(product.Category);
            createResult.Description.Should().Be(product.Description);
            createResult.Image.Should().Be(product.Image);
            createResult.Price.Should().Be(product.Price);
            createResult.Title.Should().Be(product.Title);
            createResult.Rating.Count.Should().Be(product.Rating.Count);
            createResult.Rating.Rate.Should().Be(product.Rating.Rate);
            await _unitOfWork.Received(2).Products.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Tests the createProduct action with an invalid command -> should fail")]
        public async Task Tests_CreateProduct_With_Invalid_Command()
        {
            var command = new CreateProductCommand();

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            Assert.Contains("The product title is required", exception.Message);
            Assert.Contains("The product description is required.", exception.Message);
            Assert.Contains("The product category is required.", exception.Message);
            Assert.Contains("The product price must be greater than zero.", exception.Message);
        }
    }
}
