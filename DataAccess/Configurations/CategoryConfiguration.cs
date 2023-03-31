using DataAccess.Enums;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(o => o.Id);
            builder.HasIndex(o => o.Id);

            builder.Property(o => o.Id).UseIdentityColumn();

            builder.Property(o => o.Status).HasDefaultValue(Status.Active);
        }
    }
}
