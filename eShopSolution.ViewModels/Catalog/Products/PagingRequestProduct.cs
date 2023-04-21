using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Catalog.Products
{
    public class PagingRequestProduct : PagingRequestBase
    {
        public string Keyword { get; set; }
        public List<long> CategoryIds { get; set; }
    }
}
