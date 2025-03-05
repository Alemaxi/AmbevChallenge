using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.ListCartsPaged;
using Ambev.DeveloperEvaluation.Application.Carts.Shared.CartResult;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Controller
{
    public class CartControllerTest
    {
        private readonly IMediator _mediator;
        private readonly CartsController _controller;

        public CartControllerTest()
        {
            _mediator = Substitute.For<IMediator>();
            _controller = new CartsController(_mediator);
        }

        [Fact(DisplayName = "Tests the GetAllPaged method")]
        public async Task ListCartsPaged()
        {
            var command = new ListCartsPagedCommand();

            var listCarts = new ListCartsPagedResult
            {
                Total = 10,
                cartsResult = new List<CartResult> { new CartResult(), new CartResult() }
            };

            _mediator.Send(Arg.Any<ListCartsPagedCommand>()).Returns(listCarts);

            var response = await _controller.GetAllPaged(command);

            response.Should().NotBeNull();
            (response as ObjectResult)!.StatusCode.Should().Be(200);
            (response as ObjectResult)!.Value.Should().NotBeNull();
            await _mediator.Received(1).Send(Arg.Any<ListCartsPagedCommand>());
        }

        [Fact(DisplayName = "It tests getting a cart by Id")]
        public async Task GetById()
        {
            var command = new GetCartCommand { Id = Guid.NewGuid() };

            _mediator.Send(Arg.Any<GetCartCommand>()).Returns(new GetCartResult());

            var response = await _controller.GetById(command);

            response.Should().NotBeNull();
            (response as ObjectResult)!.StatusCode.Should().Be(200);
            (response as ObjectResult)!.Value.Should().NotBeNull();
            await _mediator.Received(1).Send(Arg.Any<GetCartCommand>());
        }

        [Fact(DisplayName = "It creates a new cart")]
        public async Task CreateCart()
        {
            var command = new CreateCartCommand();

            _mediator.Send(Arg.Any<CreateCartCommand>()).Returns(new CreateCartResult());

            var response = await _controller.CreateCart(command);

            response.Should().NotBeNull();
            (response as ObjectResult)!.StatusCode.Should().Be(201);
            (response as ObjectResult)!.Value.Should().NotBeNull();
            await _mediator.Received(1).Send(Arg.Any<CreateCartCommand>());
        }

        [Fact(DisplayName = "It Updates a new cart")]
        public async Task UpdateCart()
        {
            var command = new UpdateCartCommand();

            _mediator.Send(Arg.Any<UpdateCartCommand>()).Returns(new UpdateCartResult());

            var response = await _controller.Updatecart(command);

            response.Should().NotBeNull();
            (response as ObjectResult)!.StatusCode.Should().Be(200);
            (response as ObjectResult)!.Value.Should().NotBeNull();
            await _mediator.Received(1).Send(Arg.Any<UpdateCartCommand>());
        }
    }
}
