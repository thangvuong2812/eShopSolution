using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Extensions.ModelsExtension
{
    public class ProductInCategoryBuilderExtension : IModelBuilderExtensions
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductInCategory>().HasData(
              new ProductInCategory() { SysId = 1, ProductId = 1, CategoryId = 1 }
              );

        }
    }
}
