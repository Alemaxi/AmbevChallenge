using Ambev.DeveloperEvaluation.Application.Util;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleHandler : GenericHandler<DeleteSaleCommand, string, DeleteSaleCommandValidator>
    {
        public DeleteSaleHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<string> ExecuteHandlerCode(DeleteSaleCommand request, CancellationToken cancelation)
        {
            var result = await _unitOfWork.Sales.DeleteAsync(request.Id);
            await _unitOfWork.CommitAsync();

            return result ? "Sale deleted successfully" : "Sale not found";
        }
    }
}
