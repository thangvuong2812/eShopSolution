using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
      

        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("AppUserRoles").HasKey(o => o.SysId);
            builder.HasIndex(o => o.SysId);
            builder.Property(o => o.SysId).UseIdentityColumn();
          
            //builder.HasOne(o => o.User).WithMany(o => o.UserRoles).HasForeignKey(o => o.UserId);
            //builder.HasOne(o => o.Role).WithMany(o => o.UserRoles).HasForeignKey(o => o.RoleId);
        }
    }
}
