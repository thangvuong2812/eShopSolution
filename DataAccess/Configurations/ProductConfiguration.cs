using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).UseIdentityColumn();
            builder.HasIndex(o => o.Id);
            builder.Property(o => o.Price).IsRequired();
            builder.Property(o => o.OriginalPrice).IsRequired();
            builder.Property(o => o.Stock).IsRequired().HasDefaultValue(0);
            builder.Property(o => o.ViewCount).IsRequired().HasDefaultValue(0);
        }
    }
}
