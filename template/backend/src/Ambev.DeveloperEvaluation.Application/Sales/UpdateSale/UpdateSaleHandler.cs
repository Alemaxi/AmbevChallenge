using Ambev.DeveloperEvaluation.Application.Sales.Shared.Result;
using Ambev.DeveloperEvaluation.Application.Util;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : GenericHandler<UpdateSaleCommand, UpdateSaleResult, UpdateSaleCommandValidator>
    {
        public UpdateSaleHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async override Task<UpdateSaleResult> ExecuteHandlerCode(UpdateSaleCommand request, CancellationToken cancelation)
        {
            var sale = await _unitOfWork.Sales.GetSaleWithProduct(request.Id);

            sale.Items.Clear();
            var items = request.Items.Select(i => _mapper.Map<SaleItem>(i)).ToList();
            sale.Items.AddRange(items);
            sale.IsCancelled = request.IsCancelled;
            

            var result = await _unitOfWork.Sales.UpdateAsync(sale, cancelation);
            await _unitOfWork.CommitAsync(cancelation);

            return new UpdateSaleResult { Sale = _mapper.Map<SaleResult>(result) };
        }
    }
}
