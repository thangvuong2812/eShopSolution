using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Cart
    {
        public long Id { set; get; }
        public long ProductId { set; get; }
        public long Quantity { set; get; }
        public decimal Price { set; get; }
        public long UserId { set; get; }

        public Product Product { set; get; }
        public User User { get; set; }
    }
}
