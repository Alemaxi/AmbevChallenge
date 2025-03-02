using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductResult;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct
{
    public class ListProductProfile : Profile
    {
        public ListProductProfile() 
        { 
            CreateMap<Product, ProductResult>();
        }
    }
}
