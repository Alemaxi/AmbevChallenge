using Ambev.DeveloperEvaluation.Application.Carts.Shared.CartResult;
using Ambev.DeveloperEvaluation.Application.Util;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    public class GetCartHandler : GenericHandler<GetCartCommand, GetCartResult, GetCartCommandValidator>
    {
        public GetCartHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<GetCartResult> ExecuteHandlerCode(GetCartCommand request, CancellationToken cancelation)
        {
            return new GetCartResult { Cart = _mapper.Map<CartResult>(await _unitOfWork.Carts.GetByIdAsync(request.Id)) };
        }
    }
}
