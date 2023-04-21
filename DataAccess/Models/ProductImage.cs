using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class ProductImage
    {
        public long SysId { get; set; }
        public string ImgPath { get; set; }
        public long ProductId { get; set; }
        public string Caption { get; set; }
        public long FileSize { get; set; }
        public int SortOrder { get; set; }
        public bool IsDefault { get; set; }
        public DateTime? DateCreated { get; set; }
        public long? CreatedByUserId { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
