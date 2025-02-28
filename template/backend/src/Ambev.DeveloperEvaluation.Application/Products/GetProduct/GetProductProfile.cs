using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductProfile : Profile
    {
        public GetProductProfile() 
        {
            CreateMap<GetProductCommand, Product>();
            CreateMap<Product, GetProductResult>();
        }
    }
}
