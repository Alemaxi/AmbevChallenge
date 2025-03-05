using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.Shared.CartResult;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class GetCartHandlerTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly GetCartHandler _handler;

        public GetCartHandlerTest()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();

            _handler = new GetCartHandler(_unitOfWork, _mapper);
        }

        [Fact(DisplayName = "It gets a cart successfuly")]
        public async Task GetCartSuccess()
        {
            _unitOfWork.Carts.GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(new Cart());
            _mapper.Map<CartResult>(Arg.Any<Cart>()).Returns(new CartResult());

            var result = await _handler.Handle(new GetCartCommand { Id = Guid.NewGuid() },CancellationToken.None);

            Assert.NotNull(result);
            await _unitOfWork.Received(2).Carts.GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None);
            _mapper.Received(1).Map<CartResult>(Arg.Any<Cart>());
        }

        [Fact(DisplayName = "It gets a cart failling")]
        public async Task GetCartFail()
        {

            var exception =await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(new GetCartCommand { Id = Guid.Empty }, CancellationToken.None));

            Assert.NotNull(exception);
            exception.Errors.Count().Should().Be(1);
        }
    }
}
