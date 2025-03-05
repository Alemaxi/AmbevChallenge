using Ambev.DeveloperEvaluation.Application.Util;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
    public class DeleteCartHandler : GenericHandler<DeleteCartCommand, string, DeleteCartCommandValidator>
    {
        public DeleteCartHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<string> ExecuteHandlerCode(DeleteCartCommand request, CancellationToken cancelation)
        {
            var result = await _unitOfWork.Carts.DeleteAsync(request.Id, cancelation);
            await _unitOfWork.CommitAsync(cancelation);

            return result ? $"Cart deleted" : $"Cart not deleted";
        }
    }
}
