using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Role : IdentityRole<int>
    {
        public string Description { set; get; }
    }
}
