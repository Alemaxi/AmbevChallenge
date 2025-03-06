using Ambev.DeveloperEvaluation.Application.Sales.ListSalesPaged;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Result;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class ListSalesHandlerTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly ListSalesPagedHandler _handler;

        public ListSalesHandlerTest()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();

            _handler = new ListSalesPagedHandler(_unitOfWork, _mapper);
        }

        [Fact(DisplayName = "It list sales paged successfuly")]
        public async Task ListSalesPagedSuccessfuly()
        {
            var command = new ListSalesPagedCommand();

            _unitOfWork.Sales.GetSalesPagedWithProduct(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>()).Returns(new List<Sale>());
            _unitOfWork.Sales.CountAllAsync().Returns(2);
            _mapper.Map<SaleResult>(Arg.Any<Sale>).Returns(new SaleResult());

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            _unitOfWork.Received(2);
            _mapper.Received(1);
        }

        [Fact(DisplayName = "It list sales paged unsuccessfuly")]
        public async Task ListSalesPagedUnsuccessfuly()
        {
            var command = new ListSalesPagedCommand { Page = 0, Size = 0};

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            Assert.NotNull(exception);
            exception.Errors.Count().Should().Be(2);
        }
    }
}
