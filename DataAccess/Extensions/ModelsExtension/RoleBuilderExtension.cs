using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Extensions.ModelsExtension
{
    public class RoleBuilderExtension : IModelBuilderExtensions
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = 1,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });
        }
    }
}
