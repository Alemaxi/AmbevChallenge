using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct
{
    public class ListProductCommand : IRequest<ListProductResult>
    {
        [FromQuery(Name = "_page")]
        public int Page { get; set; } = 1;
        [FromQuery(Name = "_size")]
        public int Size { get; set; } = 10;
        [FromQuery(Name = "_order")]
        public string Order { get; set; } = string.Empty;
    }
}
