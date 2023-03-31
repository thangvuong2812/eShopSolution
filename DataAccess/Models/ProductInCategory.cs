using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class ProductInCategory
    {
        public long SysId { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
