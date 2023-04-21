using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages").HasKey(o => o.SysId);
            builder.HasIndex(o => o.SysId);
            builder.Property(o => o.SysId).UseIdentityColumn();
            builder.Property(o => o.ImgPath).IsRequired();
            builder.Property(o => o.Caption).HasMaxLength(200);

            builder.HasOne(o => o.Product).WithMany(o => o.ProductImages).HasForeignKey(o => o.ProductId);
            builder.HasOne(o => o.User).WithMany(o => o.ProductImages).HasForeignKey(o => o.CreatedByUserId);
        }
    }
}
