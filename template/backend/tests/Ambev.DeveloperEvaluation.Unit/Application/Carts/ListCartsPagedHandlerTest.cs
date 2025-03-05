using Ambev.DeveloperEvaluation.Application.Carts.ListCartsPaged;
using Ambev.DeveloperEvaluation.Application.Carts.Shared.CartResult;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class ListCartsPagedHandlerTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly ListCartsPagedHandler _handler;

        public ListCartsPagedHandlerTest()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();
            _handler = new ListCartsPagedHandler(_unitOfWork, _mapper);
        }

        [Fact(DisplayName = "It tests getting a list of carts")]
        public async Task TestsListCartsPaged()
        {
            var carts = new List<Cart> { new Cart(), new Cart() };

            _unitOfWork.Carts.GetAllPaginatedAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), CancellationToken.None).Returns(carts);
            _mapper.Map<CartResult>(Arg.Any<Cart>()).Returns(new CartResult());

            var result = await _handler.Handle(new ListCartsPagedCommand(), CancellationToken.None);

            Assert.NotNull(result);
            await _unitOfWork.Received(3).Carts.GetAllPaginatedAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), CancellationToken.None);
            _mapper.Received(2).Map<CartResult>(Arg.Any<Cart>());
        }

        [Fact(DisplayName = "It tests getting a list of carts - fail")]
        public async Task TestsListCartsPagedFail()
        {
            var command = new ListCartsPagedCommand();
            command.Page = 0;
            command.Size = 0;

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            Assert.NotNull(exception);
            exception.Errors.Count().Should().Be(2);
        }
    }
}
