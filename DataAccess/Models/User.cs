using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class User : IdentityUser<long>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DOB { set; get; }

        public ICollection<Cart> Carts { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
