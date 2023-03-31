using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Order
    {
        public long Id { set; get; }
        public DateTime OrderDate { set; get; }
        public long UserId { set; get; }
        public string ShipName { set; get; }
        public string ShipAddress { set; get; }
        public string ShipEmail { set; get; }
        public int ShipPhoneNumber { set; get; }
        public OrderStatus Status { set; get; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public User User { get; set; }
    }
}
