using Ambev.DeveloperEvaluation.Application.Sales.Shared.Command;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>();

            CreateMap<SaleItemCommand, SaleItem>()
                .AfterMap((src, dest) =>
                {
                    dest.SetQuantity(src.Quantity);
                });
        }
    }
}
