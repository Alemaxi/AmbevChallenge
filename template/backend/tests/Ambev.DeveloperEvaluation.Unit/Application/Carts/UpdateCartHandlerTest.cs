using Ambev.DeveloperEvaluation.Application.Carts.Shared.Results;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
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
    public class UpdateCartHandlerTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UpdateCartHandler _handler;

        public UpdateCartHandlerTest()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateCartHandler(_unitOfWork, _mapper);
        }

        [Fact(DisplayName = "It updates a cart successfuly")]
        public async Task TestsUpdateCartSuccesfuly()
        {
            var command = UpdateCartHandlerTestData.GenerateValidCommand();

            _unitOfWork.Carts.UpdateAsync(Arg.Any<Cart>()).Returns(new Cart());
            _mapper.Map<Cart>(Arg.Any<UpdateCartCommand>()).Returns(new Cart());
            _mapper.Map<CartResult>(Arg.Any<Cart>()).Returns(new CartResult());

            var result = _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            _unitOfWork.Received(3);
            _mapper.Received(2);
        }

        [Fact(DisplayName = "It fails to update a cart")]
        public async Task TestsUpdateCartUnsuccesfuly()
        {
            var command = new UpdateCartCommand();

            var excepiton = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            excepiton.Should().NotBeNull();
            excepiton.Errors.Count().Should().Be(3);
        }
    }
}
