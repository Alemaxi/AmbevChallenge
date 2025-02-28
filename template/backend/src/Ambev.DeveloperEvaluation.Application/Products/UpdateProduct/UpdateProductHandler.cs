
using Ambev.DeveloperEvaluation.Application.Util;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductHandler : GeneralHandler<UpdateProductCommand, UpdateProductResult, UpdateProductCommandValidator>
    {
        public override Task<UpdateProductResult> ExecuteHandlerCode(UpdateProductCommand Request, CancellationToken )
        {
            throw new NotImplementedException();
        }
    }
}
