using Ambev.DeveloperEvaluation.Application.Util;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    internal class DeleteProductHandler : GenericHandler<DeleteProductCommand, bool, DeleteProductCommandValidator>
    {
        public DeleteProductHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<bool> ExecuteHandlerCode(DeleteProductCommand request, CancellationToken cancelation)
        {
            var result = await _unitOfWork.Products.DeleteAsync(request.Id, cancelation);
            await _unitOfWork.CommitAsync(cancelation);
            return result;
        }
    }
}
