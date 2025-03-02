using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductProfile : Profile
    {
        public UpdateProductProfile(string profileName) : base(profileName)
        {
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<Product, UpdateProductResult>();
        }
    }
}
