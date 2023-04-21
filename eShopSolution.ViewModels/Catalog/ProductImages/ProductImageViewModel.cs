using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Catalog.ProductImages
{
    public class ProductImageViewModel
    {
        public long SysId { get; set; }
        public string ImgPath { get; set; }
        public long ProductId { get; set; }
        public string Caption { get; set; }
        public decimal FileSize { get; set; }
        public int SortOrder { get; set; }
        public bool IsDefault { get; set; }
        public DateTime? DateCreated { get; set; }
        public long? CreatedByUserId { get; set; }
    }
}
