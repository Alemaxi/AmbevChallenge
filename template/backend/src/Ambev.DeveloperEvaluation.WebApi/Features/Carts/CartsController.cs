using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.ListCartsPaged;
using Ambev.DeveloperEvaluation.Application.Carts.Shared.CartResult;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts
{
    public class CartsController : BaseController
    {
        private readonly IMediator _mediator;

        public CartsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaged(ListCartsPagedCommand command)
        {
            var result = await _mediator.Send(command);

            return OkPaginated(new PaginatedList<CartResult>(result.cartsResult, result.Total, command.Page, command.Size));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(GetCartCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(new ApiResponseWithData<CartResult>
            {
                Success = true,
                Message = "Product created successfully",
                Data = result.Cart
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart([FromBody] CreateCartCommand command)
        {
            var result = await _mediator.Send(command);

            return Created(string.Empty, new ApiResponseWithData<CartResult>
            {
                Success = true,
                Message = "Product created successfully",
                Data = result.Cart
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Updatecart(UpdateCartCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(new ApiResponseWithData<CartResult>
            {
                Success = true,
                Message = "Product created successfully",
                Data = result.Cart
            });
        }

        [HttpDelete]
        public Task<IActionResult> DeleteCart(DeleteCartCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
