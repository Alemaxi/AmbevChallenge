using Ambev.DeveloperEvaluation.Application.Carts.Shared.CartResult;
using Ambev.DeveloperEvaluation.Application.Util;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartHandler : GenericHandler<UpdateCartCommand, UpdateCartResult, UpdateCartCommandValidator>
    {
        public UpdateCartHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<UpdateCartResult> ExecuteHandlerCode(UpdateCartCommand request, CancellationToken cancelation)
        {
            var result = await _unitOfWork.Carts.UpdateAsync(_mapper.Map<Cart>(request),cancelation);
            await _unitOfWork.CommitAsync(cancelation);

            return new UpdateCartResult { Cart = _mapper.Map<CartResult>(result) };
        }
    }
}
