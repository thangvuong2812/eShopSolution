using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class ProductTranslationConfiguration : IEntityTypeConfiguration<ProductTranslation>
    {
        public void Configure(EntityTypeBuilder<ProductTranslation> builder)
        {
            builder.ToTable("ProductTranslations").HasKey(o => o.Id);
            builder.HasIndex(o => o.Id);
            builder.Property(o => o.Id).UseIdentityColumn();

            builder.Property(o => o.Name).IsRequired().HasMaxLength(200);
            builder.Property(o => o.SeoAlias).IsRequired().HasMaxLength(200);
            builder.Property(o => o.Details).HasMaxLength(500);
            builder.Property(o => o.LanguageId).IsUnicode(false).IsRequired().HasMaxLength(5);
            builder.HasOne(o => o.Language).WithMany(o => o.ProductTranslations).HasForeignKey(o => o.LanguageId);
            builder.HasOne(o => o.Product).WithMany(o => o.ProductTranslations).HasForeignKey(o => o.ProductId);



        }
    }
}
