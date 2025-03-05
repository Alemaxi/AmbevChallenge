using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class DeleteCartHandlerTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DeleteCartHandler _handler;

        public DeleteCartHandlerTest()
        {
            _mapper = Substitute.For<IMapper>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _handler = new DeleteCartHandler(_unitOfWork, _mapper);
        }


        [Fact(DisplayName = "It tests deleting a cart successfuly")]
        public async Task TestDeleteCartSuccessfuly()
        {
            _unitOfWork.Carts.DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);

            var result = await _handler.Handle(new DeleteCartCommand { Id = Guid.NewGuid() },CancellationToken.None);

            Assert.NotNull(result);
            result.Should().BeAssignableTo<string>();
            _unitOfWork.Received(2);
        }

        [Fact(DisplayName = "It tests deleting a cart unsuccessfuly")]
        public async Task TestDeleteCartUnsuccessfuly()
        {

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(new DeleteCartCommand { Id = Guid.Empty }, CancellationToken.None));

            Assert.NotNull(exception);
            exception.Errors.Count().Should().Be(1);
        }
    }
}
