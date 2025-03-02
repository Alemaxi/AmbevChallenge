using Ambev.DeveloperEvaluation.Application.Util;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Threading;
using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductResult;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct
{
    public class ListProductHandler : GenericHandler<ListProductCommand, ListProductResult, ListProductCommandValidator>
    {

        public ListProductHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async override Task<ListProductResult> ExecuteHandlerCode(ListProductCommand request, CancellationToken cancelation)
        {
            var result = await _unitOfWork.Products.GetAllPaginatedAsync(request.Page, request.Size, request.Order, cancelation);
            return new ListProductResult
            {
                Count = result.Count(),
                Page = request.Page,
                Size = request.Size,
                Produtos = result.Select(product => _mapper.Map<ProductResult>(product)).ToList()
            };
        }
    }
}
