using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class CreateCartHandlerTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CreateCartHandler _handler;

        public CreateCartHandlerTest()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateCartHandler(_unitOfWork, _mapper);
        }

        [Fact(DisplayName = "It creates a new Cart")]
        public async Task TestsCreateCartSuccess()
        {
            var command = CreateCartHandlerTestData.GenerateValidCommand();

            _unitOfWork.Carts.CreateAsync(Arg.Any<Cart>(), CancellationToken.None).Returns(new Cart());
            _mapper.Map<Cart>(Arg.Any<CreateCartCommand>()).Returns(new Cart());
            _mapper.Map<CartResult>(Arg.Any<Cart>()).Returns(new CartResult());

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Cart.Should().NotBeNull();
            _unitOfWork.Received(2).Carts.CreateAsync(Arg.Any<Cart>(), CancellationToken.None);
            _mapper.Received(2);
        }

        [Fact(DisplayName = "It fails to create a new Cart")]
        public async Task TestsCreateCartFail()
        {
            var command = new CreateCartCommand();

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            exception.Should().NotBeNull();
            exception.Errors.Count().Should().Be(2);
        }
    }
}
