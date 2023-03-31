using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Permission
    {
        public long Id { set; get; }
        public long RoleId { set; get; }
        public long FunctionId { set; get; }
        public long ActionId { set; get; }
    }
}
