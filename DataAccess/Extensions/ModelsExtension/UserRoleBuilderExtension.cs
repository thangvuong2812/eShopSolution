using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Extensions.ModelsExtension
{
    public class UserRoleBuilderExtension : IModelBuilderExtensions
    {
        public void Seed(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserRole>().HasData(new UserRole
            {
                SysId = 1,
                RoleId = 1,
                UserId = 1
            });

        }
    }
}
