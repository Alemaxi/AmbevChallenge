using Ambev.DeveloperEvaluation.Application.Sales.Shared.result;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Result;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Shared
{
    public class SharedProfile : Profile
    {
        public SharedProfile() 
        {
            CreateMap<Sale, SaleResult>().ForMember(x => x.TotalAmount, y => y.MapFrom(x => x.GetTotalSaleAmount()));
            CreateMap<SaleItem, SaleItemResult>().ForMember(x => x.TotalAmount, y => y.MapFrom(x => x.GetTotalAmount()));
        }
    }
}
