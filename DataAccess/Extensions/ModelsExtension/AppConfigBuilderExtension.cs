using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Extensions.ModelsExtension
{
    public class AppConfigBuilderExtension : IModelBuilderExtensions
    {

        public void Seed(ModelBuilder _modelBuilder)
        {
            _modelBuilder.Entity<AppConfig>().HasData(
               new AppConfig() { SysId = 1, Key = "HomeTitle", Value = "This is home page of eShopSolution" },
               new AppConfig() { SysId = 2, Key = "HomeKeyword", Value = "This is keyword of eShopSolution" },
               new AppConfig() { SysId = 3, Key = "HomeDescription", Value = "This is description of eShopSolution" }
               );
        }
    }
}
