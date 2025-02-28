using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class CreateProductHandlerTest
    {
        private readonly IProductRepository _productRepository;
        private readonly CreateProductHandler _handler;
        private readonly IMapper _mapper;

        public CreateProductHandlerTest()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateProductHandler(_productRepository, _mapper);
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

            _productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
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
            await _productRepository.Received(1).CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        }
    }
}
