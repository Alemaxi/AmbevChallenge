using Ambev.DeveloperEvaluation.Application.Util;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductHandler : GenericHandler<GetProductCommand, GetProductResult, GetProductCommandValidator>
    {

        public GetProductHandler(IUnitOfWork unitOfWork, IMapper mapper): base(unitOfWork,mapper)
        {
        }

        public override async Task<GetProductResult> ExecuteHandlerCode(GetProductCommand request, CancellationToken cancelation)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.Id);

            return _mapper.Map<GetProductResult>(product);
        }
    }
}
