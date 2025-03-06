using Ambev.DeveloperEvaluation.Application.Sales.Shared.Result;
using Ambev.DeveloperEvaluation.Application.Util;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleHandler : GenericHandler<GetSaleCommand, GetSaleResult, GetSaleCommandValidator>
    {
        public GetSaleHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<GetSaleResult> ExecuteHandlerCode(GetSaleCommand request, CancellationToken cancelation)
        {
            return new GetSaleResult { sale = _mapper.Map<SaleResult>(await _unitOfWork.Sales.GetByIdAsync(request.Id)) };
        }
    }
}
