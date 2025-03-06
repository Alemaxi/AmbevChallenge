using Ambev.DeveloperEvaluation.Application.Sales.Shared.Command;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public DateTime SaleDate { get; set; }

        public Guid CustomerId { get; set; }


        public string Branch { get; set; } = string.Empty;

        public List<SaleItemCommand> Items { get; set; } = new List<SaleItemCommand>();
    }
}
