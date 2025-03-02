using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductResult;
using Ambev.DeveloperEvaluation.Application.Util;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory
{
    public class ListProductsByCategoryHandler : GenericHandler<ListProductsByCategoryCommand, ListProductsByCategoryResult, ListProductsByCategoryCommandValidator>
    {
        public ListProductsByCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<ListProductsByCategoryResult> ExecuteHandlerCode(ListProductsByCategoryCommand request, CancellationToken cancelation)
        {
            var result = await _unitOfWork.Products.ListProductsByCategory(request.Category, request.Page, request.Size, request.Order);

            return new ListProductsByCategoryResult
            {
                Produtos = result.Select(p => _mapper.Map<ProductResult>(p)).ToList(),
                Count = result.Count,
                Page = request.Page,
                Size = request.Size
            };
        }
    }
}
