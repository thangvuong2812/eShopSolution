using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.ToTable("ProductInCategories");
            builder.HasKey(o => o.SysId);
            builder.HasIndex(o => o.SysId);

            builder.Property(o => o.SysId).UseIdentityColumn();

            builder.HasOne(o => o.Product).WithMany(o => o.ProductInCategories).HasForeignKey(o => o.ProductId);
            builder.HasOne(o => o.Category).WithMany(o => o.ProductInCategories).HasForeignKey(o => o.CategoryId);
        }
    }
}
