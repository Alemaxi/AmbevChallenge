using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class DeleteSaleHandlerTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly DeleteSaleHandler _handler;

        public DeleteSaleHandlerTest()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();

            _handler = new DeleteSaleHandler(_unitOfWork, _mapper);
        }

        //testar se a venda foi deletada com sucesso
        [Fact(DisplayName = "It delete sale successfuly")]
        public async Task DeleteSaleSuccessfuly()
        {
            var command = new DeleteSaleCommand { Id = Guid.NewGuid()};

            _unitOfWork.Sales.GetByIdAsync(Arg.Any<Guid>()).Returns(new Sale());
            _unitOfWork.Sales.DeleteAsync(Arg.Any<Guid>()).Returns(true);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            _unitOfWork.Received(2);
        }

        [Fact(DisplayName = "It delete sale unsuccessfuly")]
        public async Task DeleteSaleUnsuccessfuly()
        {
            var command = new DeleteSaleCommand { Id = Guid.Empty };

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            Assert.NotNull(exception);
            exception.Errors.Count().Should().Be(1);
        }
    }
}
