using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;
using Ambev.DeveloperEvaluation.Application.Products.ListCategories;
using Ambev.DeveloperEvaluation.Application.Products.ListProduct;
using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductResult;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
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
            var result = await _mediator.Send(command);

            return Ok(new ApiResponseWithData<GetProductResult>
            {
                Success = true,
                Message = "Product modified successfully",
                Data = result
            });
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListProducts([FromQuery] ListProductCommand command)
        {
            var result = await _mediator.Send(command);

            return OkPaginated(new PaginatedList<ProductResult>(result.Produtos, result.Count, command.Page, command.Size));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand productCommand)
        {
            var result = await _mediator.Send(productCommand);

            return Created(string.Empty, new ApiResponseWithData<CreateProductResult>
            {
                Success = true,
                Message = "Product created successfully",
                Data = result
            });
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductCommand productCommand)
        {
            productCommand.SetId(id);
            var result = await _mediator.Send(productCommand);

            return Created(string.Empty, new ApiResponseWithData<UpdateProductResult>
            {
                Success = true,
                Message = "Product created successfully",
                Data = result
            });
        }

        [HttpGet("categories")]
        public async Task<IActionResult> ListCategories()
        {
            var result = await _mediator.Send(new ListCategoriesCommand());

            return Ok(new ApiResponseWithData<List<string>>
            {
                Success = true,
                Message = "Product modified successfully",
                Data = result
            });
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> ListProductsByCategory(ListProductsByCategoryCommand command)
        {
            var result = await _mediator.Send(command);

            return OkPaginated(new PaginatedList<ProductResult>(result.Produtos, result.Count, command.Page, command.Size));
        }
    }
}
