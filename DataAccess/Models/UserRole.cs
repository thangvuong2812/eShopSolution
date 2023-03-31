using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class UserRole
    {
        public long Id { set; get; }
        public long UserId { set; get; }
        public long RoleId { set; get; }
    }
}
