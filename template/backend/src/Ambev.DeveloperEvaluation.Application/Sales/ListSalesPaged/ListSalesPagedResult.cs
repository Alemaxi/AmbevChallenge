using Ambev.DeveloperEvaluation.Application.Sales.Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSalesPaged
{
    public class ListSalesPagedResult
    {
        public int TotalOfRegisters { get; set; }
        public List<SaleResult> Sales { get; set; }
    }
}
