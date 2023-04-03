using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class UserRole : IdentityUserRole<long>
    {
        public long SysId { set; get; }
   
        public Role Role { get; set; }
        public User User { get; set; }
    }
}
