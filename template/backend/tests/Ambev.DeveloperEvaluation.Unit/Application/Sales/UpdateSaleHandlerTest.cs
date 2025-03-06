using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Command;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Result;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class UpdateSaleHandlerTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UpdateSaleHandler _handler;

        public UpdateSaleHandlerTest()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateSaleHandler(_unitOfWork, _mapper);
        }

        [Fact(DisplayName = "It tests creating a sale successfuly")]
        public async Task UpdateSaleSuccessfuly()
        {
            var command = UpdateSaleHandlerTestData.GenerateValidCommand();

            _mapper.Map<Sale>(Arg.Any<UpdateSaleCommand>()).Returns(new Sale());
            _mapper.Map<SaleResult>(Arg.Any<Sale>()).Returns(new SaleResult());
            _mapper.Map<SaleItem>(Arg.Any<SaleItemCommand>()).Returns(new SaleItem());
            _unitOfWork.Sales.GetSaleWithProduct(Arg.Any<Guid>()).Returns(new Sale());
            _unitOfWork.Sales.UpdateAsync(Arg.Any<Sale>()).Returns(new Sale());

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            result.Sale.Should().BeAssignableTo<SaleResult>();
            _mapper.Received(3);
            _unitOfWork.Received(4);
        }

        [Fact(DisplayName = "It tests creating a sale unsuccessfuly")]
        public async Task UpdateSaleUnsuccessfuly()
        {
            var command = new UpdateSaleCommand();

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            Assert.NotNull(exception);
            exception.Errors.Count().Should().Be(2);
        }
    }
}
