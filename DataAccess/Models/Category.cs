using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Category
    {
        public long Id { set; get; }
        public int SortOrder { set; get; }
        public bool IsShowOnHome { set; get; }
        public long? ParentId { set; get; }
        public Status Status { set; get; }
        public ICollection<ProductInCategory> ProductInCategories { get; set; }
        public ICollection<CategoryTranslation> CategoryTranslations { get; set; }
    }
}
