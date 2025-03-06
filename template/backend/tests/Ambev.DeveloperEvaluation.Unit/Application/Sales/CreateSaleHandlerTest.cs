using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Result;
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
    public class CreateSaleHandlerTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTest()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();

            _handler = new CreateSaleHandler(_unitOfWork, _mapper);
        }

        [Fact(DisplayName = "It tests creating a sale successfuly")]
        public async Task CreateSaleSuccessfuly()
        {
            var command = CreateSaleHandlerTestData.GenerateValidCommand();

            _mapper.Map<Sale>(Arg.Any<CreateSaleCommand>()).Returns(new Sale());
            _mapper.Map<SaleResult>(Arg.Any<Sale>()).Returns(new SaleResult());
            _unitOfWork.Sales.CreateAsync(Arg.Any<Sale>()).Returns(new Sale());

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            result.Sale.Should().BeAssignableTo<SaleResult>();
            _mapper.Received(2);
            _unitOfWork.Received(2);
        }

        [Fact(DisplayName = "It tests creating a sale unsuccessfuly")]
        public async Task CreateSaleUnsuccessfuly()
        {
            var command = new CreateSaleCommand();

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            Assert.NotNull(exception);
            exception.Errors.Count().Should().Be(3);
        }
    }
}
