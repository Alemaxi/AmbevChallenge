using Ambev.DeveloperEvaluation.Application.Sales.Shared.Result;
using Ambev.DeveloperEvaluation.Application.Util;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSalesPaged
{
    public class ListSalesPagedHandler : GenericHandler<ListSalesPagedCommand, ListSalesPagedResult, ListSalesPagedCommandValidator>
    {
        public ListSalesPagedHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<ListSalesPagedResult> ExecuteHandlerCode(ListSalesPagedCommand request, CancellationToken cancelation)
        {
            var result = await _unitOfWork.Sales.GetAllPaginatedAsync(request.Page, request.Size, request.Order, cancelation);

            return new ListSalesPagedResult { Sales = _mapper.Map<List<SaleResult>>(result), TotalOfRegisters = await _unitOfWork.Sales.CountAllAsync() };
        }
    }
}
