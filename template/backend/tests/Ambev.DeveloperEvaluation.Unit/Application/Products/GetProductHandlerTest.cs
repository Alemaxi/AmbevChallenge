using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
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
    public class GetProductHandlerTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly GetProductHandler _handler;

        public GetProductHandlerTest()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();

            _handler = new GetProductHandler(_unitOfWork, _mapper);
        }
        [Fact(DisplayName = "Tests GetProductResult action passing a valid Guid ID")]
        public async Task Tests_GetProduct_With_Valid_Id()
        {
            var handler = new GetProductHandler(_unitOfWork, _mapper);
            var id = Guid.NewGuid();

            _unitOfWork.Products.GetByIdAsync(Arg.Any<Guid>()).Returns(new Product());
            _mapper.Map<GetProductResult>(Arg.Any<Product>()).Returns(new GetProductResult());

            var returned = await handler.Handle(new GetProductCommand { Id = id }, CancellationToken.None);

            returned.Should().NotBeNull();
            await _unitOfWork.Received(2).Products.GetByIdAsync(Arg.Any<Guid>());
            _mapper.Received(1).Map<GetProductResult>(Arg.Any<Product>());
        }


        [Fact(DisplayName = "Tests GetProductResult action passing an invalid Guid ID")]
        public async Task Tests_GetProduct_With_Invalid_Id()
        {
            var id = Guid.Empty;


            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(new GetProductCommand { Id = id }, CancellationToken.None));

            Assert.Contains("Invalid Id", exception.Message);
            Assert.Single(exception.Errors);
        }
    }
}
