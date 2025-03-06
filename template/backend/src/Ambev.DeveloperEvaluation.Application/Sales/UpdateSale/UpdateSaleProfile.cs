using Ambev.DeveloperEvaluation.Application.Sales.Shared.Command;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleCommand, Sale>();
            CreateMap<SaleItemCommand, SaleItem>().AfterMap((src, dest) =>
            {
                dest.SetQuantity(src.Quantity);
            });
        }
    }
}
