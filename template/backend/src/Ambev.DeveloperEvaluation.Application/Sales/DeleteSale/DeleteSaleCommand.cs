using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleCommand : IRequest<string>
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
    }
}
