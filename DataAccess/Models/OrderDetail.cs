using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class OrderDetail
    {
        public long SysId { get; set; }
        public long OrderId { set; get; }
        public long ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }

        public Order Order { get; set; }
        public Product Product{ get; set; }
    }
}
