using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSalesPaged
{
    public class ListSalesPagedCommand : IRequest<ListSalesPagedResult>
    {
        [FromQuery(Name = "_page")]
        public int Page { get; set; } = 1;
        [FromQuery(Name = "_size")]
        public int Size { get; set; } = 10;
        [FromQuery(Name = "_order")]
        public string Order { get; set; } = string.Empty;
    }
}
