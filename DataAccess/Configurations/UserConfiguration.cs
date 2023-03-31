using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(o => o.FirstName).IsRequired().HasMaxLength(200);
            builder.Property(o => o.LastName).IsRequired().HasMaxLength(200);
            builder.Property(o => o.DOB).IsRequired();
        }
    }
}
