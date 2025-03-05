using Ambev.DeveloperEvaluation.Application.Carts.Shared.CartResult;
using Ambev.DeveloperEvaluation.Application.Util;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCartsPaged
{
    public class ListCartsPagedHandler : GenericHandler<ListCartsPagedCommand, ListCartsPagedResult, ListCartsPagedCommandValidator>
    {
        public ListCartsPagedHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<ListCartsPagedResult> ExecuteHandlerCode(ListCartsPagedCommand request, CancellationToken cancelation)
        {
            var carts = await _unitOfWork.Carts.GetAllPaginatedAsync(request.Page, request.Size, request.Order);

            var result = new ListCartsPagedResult
            {
                Total = await _unitOfWork.Carts.CountAllAsync(cancelation),
                cartsResult = carts.Select(c => _mapper.Map<CartResult>(c)).ToList()
            };

            return result;
        }
    }
}
