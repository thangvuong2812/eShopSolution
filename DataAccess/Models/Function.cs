using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Function
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string Url { set; get; }
        public long ParentId { set; get; }
        //public FunctionStatus Status { set; get; }
        public int SortOrder { set; get; }
    }
}
