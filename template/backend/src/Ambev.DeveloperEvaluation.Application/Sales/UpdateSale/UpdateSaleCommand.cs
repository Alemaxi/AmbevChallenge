using Ambev.DeveloperEvaluation.Application.Sales.Shared.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        public Guid Id { get; private set; }
        

        public bool IsCancelled { get; set; } = false;

        public List<SaleItemCommand> Items { get; set; } = new List<SaleItemCommand>();

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
