using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductResult;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProductsByCategory
{
    public class ListProductsByCategoryProfile : Profile
    {
        public ListProductsByCategoryProfile()
        {
            CreateMap<Product, ProductResult>();

        }
    }
}
