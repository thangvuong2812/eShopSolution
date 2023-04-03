using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Role : IdentityRole<long>
    {
        public string Description { set; get; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
