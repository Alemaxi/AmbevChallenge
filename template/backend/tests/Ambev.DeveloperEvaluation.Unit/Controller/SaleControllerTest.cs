using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.ListSalesPaged;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Result;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Controller
{
    public class SaleControllerTest
    {
        private readonly IMediator _mediator;
        private readonly SalesController _controller;

        public SaleControllerTest()
        {
            _mediator = Substitute.For<IMediator>();
            _controller = new SalesController(_mediator);
        }


        [Fact(DisplayName = "Tests the GetAllPaged method")]
        public async Task ListSalesPaged()
        {
            var command = new ListSalesPagedCommand();

            var listSales = new ListSalesPagedResult
            {
                TotalOfRegisters = 10,
                Sales = new List<SaleResult> { new SaleResult(), new SaleResult() }
            };

            _mediator.Send(Arg.Any<ListSalesPagedCommand>()).Returns(listSales);

            var response = await _controller.ListPaged(command);

            response.Should().NotBeNull();
            (response as ObjectResult)!.StatusCode.Should().Be(200);
            (response as ObjectResult)!.Value.Should().NotBeNull();
            await _mediator.Received(1).Send(Arg.Any<ListSalesPagedCommand>());
        }

        [Fact(DisplayName = "It tests getting a sale by Id")]
        public async Task GetById()
        {
            var command = new GetSaleCommand { Id = Guid.NewGuid() };

            _mediator.Send(Arg.Any<GetSaleCommand>()).Returns(new GetSaleResult());

            var response = await _controller.GetById(command);

            response.Should().NotBeNull();
            (response as ObjectResult)!.StatusCode.Should().Be(200);
            (response as ObjectResult)!.Value.Should().NotBeNull();
            await _mediator.Received(1).Send(Arg.Any<GetSaleCommand>());
        }

        [Fact(DisplayName = "It tests creating a sale")]
        public async Task Create()
        {
            var command = new CreateSaleCommand();

            _mediator.Send(Arg.Any<CreateSaleCommand>()).Returns(new CreateSaleResult());

            var response = await _controller.Create(command);

            response.Should().NotBeNull();
            (response as ObjectResult)!.StatusCode.Should().Be(200);
            (response as ObjectResult)!.Value.Should().NotBeNull();
            await _mediator.Received(1).Send(Arg.Any<CreateSaleCommand>());
        }

        [Fact(DisplayName = "It tests Udating a sale")]
        public async Task Update()
        {
            var command = new UpdateSaleCommand();

            _mediator.Send(Arg.Any<UpdateSaleCommand>()).Returns(new UpdateSaleResult());

            var response = await _controller.Update(Guid.NewGuid(), command);

            response.Should().NotBeNull();
            (response as ObjectResult)!.StatusCode.Should().Be(200);
            (response as ObjectResult)!.Value.Should().NotBeNull();
            await _mediator.Received(1).Send(Arg.Any<UpdateSaleCommand>());
        }

        [Fact(DisplayName = "It tests deleting a sale")]
        public async Task Delete()
        {
            var command = new DeleteSaleCommand();

            _mediator.Send(Arg.Any<DeleteSaleCommand>()).Returns("deletado");

            var response = await _controller.Delete(command);

            response.Should().NotBeNull();
            (response as ObjectResult)!.StatusCode.Should().Be(200);
            (response as ObjectResult)!.Value.Should().NotBeNull();
            await _mediator.Received(1).Send(Arg.Any<DeleteSaleCommand>());
        }
    }
}
