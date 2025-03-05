using Ambev.DeveloperEvaluation.Application.Carts.Shared.CartResult;
using Ambev.DeveloperEvaluation.Application.Util;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartHandler : GenericHandler<CreateCartCommand, CreateCartResult, CreateCartCommandValidator>
    {
        public CreateCartHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<CreateCartResult> ExecuteHandlerCode(CreateCartCommand request, CancellationToken cancelation)
        {
            var result = await _unitOfWork.Carts.CreateAsync(_mapper.Map<Cart>(request),cancelation);
            await _unitOfWork.CommitAsync(cancelation);

            return new CreateCartResult { Cart = _mapper.Map<CartResult>(result) };
        }
    }
}
