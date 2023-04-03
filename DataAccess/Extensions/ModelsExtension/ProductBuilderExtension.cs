using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Extensions.ModelsExtension
{
    public class ProductBuilderExtension : IModelBuilderExtensions
    {

        public void Seed(ModelBuilder _modelBuilder)
        {
            _modelBuilder.Entity<Product>().HasData(new Product()
            {
                Id = 1,
                DateCreated = DateTime.Now,
                OriginalPrice = 100000,
                Price = 200000,
                Stock = 0,
                ViewCount = 0,
            });

        }
    }
}
