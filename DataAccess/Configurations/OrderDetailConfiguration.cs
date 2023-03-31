using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails").HasKey(o => o.SysId);
            builder.Property(o => o.SysId).UseIdentityColumn();
            builder.HasIndex(o => o.SysId);
            builder.HasOne(o => o.Order).WithMany(o => o.OrderDetails).HasForeignKey(o => o.OrderId);
            builder.HasOne(o => o.Product).WithMany(o => o.OrderDetails).HasForeignKey(o => o.ProductId);
        }
    }
}
