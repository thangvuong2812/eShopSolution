using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable("Languages");
            builder.HasKey(o => o.Id);
            builder.HasIndex(o => o.Id);

            builder.Property(o => o.Id).IsRequired().IsUnicode(false).HasMaxLength(5);

            builder.Property(o => o.Name).IsRequired().HasMaxLength(30);

        }
    }
}
