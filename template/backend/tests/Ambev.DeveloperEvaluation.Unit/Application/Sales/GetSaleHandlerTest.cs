using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Result;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class GetSaleHandlerTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly GetSaleHandler _handler;

        public GetSaleHandlerTest()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetSaleHandler(_unitOfWork, _mapper);
        }

        [Fact(DisplayName = "It tests getting sale by id successfuly")]
        public async Task GetSaleSuccessfuly()
        {
            var command = new GetSaleCommand { Id = Guid.NewGuid() };

            _unitOfWork.Sales.GetByIdAsync(Arg.Any<Guid>()).Returns(new Sale());
            _mapper.Map<SaleResult>(command).Returns(new SaleResult());

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            _unitOfWork.Received(1);
            _mapper.Received(1);
        }

        [Fact(DisplayName = "It tests getting sale by id unsuccessfuly")]
        public async Task GetSaleUnsuccessfuly()
        {
            var command = new GetSaleCommand { Id = Guid.Empty };

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            Assert.NotNull(exception);
            exception.Errors.Count().Should().Be(1);
        }
    }
}
