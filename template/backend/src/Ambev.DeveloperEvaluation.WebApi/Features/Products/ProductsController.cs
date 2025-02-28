using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.ListProduct;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] GetProductCommand command)
        {
            var validator = new GetProductCommandValidator();
            await validator.ValidateAndThrowAsync(command);

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListProducts([FromQuery] ListProductCommand command)
        {
            var validator = new ListProductCommandValidator();
            await validator.ValidateAndThrowAsync(command);

            var result = await _mediator.Send(command);

            return OkPaginated(new PaginatedList<ProductResult>(result.Produtos, result.Count, command.Page, command.Size));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand productCommand)
        {
            var validator = new CreateProductCommandValidator();
            await validator.ValidateAndThrowAsync(productCommand);

            var result = await _mediator.Send(productCommand);

            return Created(string.Empty,new ApiResponseWithData<CreateProductResult>
            {
                Success = true,
                Message = "Product created successfully",
                Data = result
            });
        }
    }
}
