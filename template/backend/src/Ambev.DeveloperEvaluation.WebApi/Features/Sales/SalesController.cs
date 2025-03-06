using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.ListSalesPaged;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Result;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    public class SalesController : BaseController
    {
        private readonly IMediator _mediator;
        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ListPaged(ListSalesPagedCommand command)
        {
            var result = await _mediator.Send(command);

            return OkPaginated(new PaginatedList<SaleResult>(result.Sales, result.TotalOfRegisters, command.Page, command.Size));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(GetSaleCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(new ApiResponseWithData<SaleResult> 
            {
                Success = true,
                Errors = null,
                Data = result.sale
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateSaleCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(new ApiResponseWithData<SaleResult>
            {
                Success = true,
                Errors = null,
                Data = result.Sale
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateSaleCommand command)
        {
            command.SetId(id);
            var result = await _mediator.Send(command);

            return Ok(new ApiResponseWithData<SaleResult>
            {
                Success = true,
                Errors = null,
                Data = result.Sale
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteSaleCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(new ApiResponseWithData<string>
            {
                Success = true,
                Errors = null,
                Data = result
            });
        }
    }
}
