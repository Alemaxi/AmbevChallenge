using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductRatingCommandShared;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class UpdateProductHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UpdateProductHandler handler;

        public UpdateProductHandlerTest()
        {
            _mapper = Substitute.For<IMapper>();
            _unitOfWork = Substitute.For<IUnitOfWork>();

            handler = new UpdateProductHandler(_unitOfWork, _mapper);
        }

        [Fact(DisplayName = "Testing UpdateProduct when success with a valid command")]
        public async void Can_update_product_success()
        {
            var command = UpdateProductHandlertestData.GenerateValidCommand();

            var entity = new Product
            {
                Id = Guid.NewGuid(),
                Category = command.Category,
                CreatedAt = DateTime.UtcNow,
                Description = command.Description,
                Image = command.Image,
                Price = command.Price,
                Title = command.Title,
                UpdatedAt = DateTime.UtcNow,
                Rating = new ProductRating
                {
                    Count = command.Rating.Count,
                    Rate = command.Rating.Rate,
                },
            };

            var result = new UpdateProductResult
            {
                Id = entity.Id,
                Category = entity.Category,
                Description = entity.Description,
                Image = entity.Image,
                Price = entity.Price,
                Title = entity.Title,
                Rating = new ProductRatingResult
                {
                    Count = entity.Rating.Count,
                    Rate = entity.Rating.Rate,
                }
            };

            _mapper.Map<Product>(Arg.Any<UpdateProductCommand>()).Returns(entity);
            _mapper.Map<UpdateProductResult>(Arg.Any<Product>()).Returns(result);

            _unitOfWork.Products.UpdateAsync(Arg.Any<Product>()).Returns(entity);

            var returned = await handler.Handle(command, CancellationToken.None);

            returned.Should().NotBeNull();
            returned.Category.Should().Be(entity.Category);
            returned.Description.Should().Be(entity.Description);
            returned.Id.Should().Be(entity.Id);
            returned.Image.Should().Be(entity.Image);
            returned.Title.Should().Be(entity.Title);
        }

        [Fact(DisplayName = "Testing handler when receiving a invalid command")]
        public async void Can_update_product_fail()
        {
            var command = new UpdateProductCommand();

            var exception = await Assert.ThrowsAnyAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));

            Assert.NotEmpty(exception.Errors);
        }
    }
}
