using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Extensions.ModelsExtension
{
    public class ProductTranslationBuilderExtension : IModelBuilderExtensions
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductTranslation>().HasData(
            new ProductTranslation()
            {
                Id = 1,
                ProductId = 1,
                Name = "Áo sơ mi nam trắng Việt Tiến",
                LanguageId = "vi",
                SeoAlias = "ao-so-mi-nam-trang-viet-tien",
                SeoDescription = "Áo sơ mi nam trắng Việt Tiến",
                SeoTitle = "Áo sơ mi nam trắng Việt Tiến",
                Details = "Áo sơ mi nam trắng Việt Tiến",
                Description = "Áo sơ mi nam trắng Việt Tiến"
            },
            new ProductTranslation()
            {
                Id = 2,
                ProductId = 1,
                Name = "Viet Tien Men T-Shirt",
                LanguageId = "en",
                SeoAlias = "viet-tien-men-t-shirt",
                SeoDescription = "Viet Tien Men T-Shirt",
                SeoTitle = "Viet Tien Men T-Shirt",
                Details = "Viet Tien Men T-Shirt",
                Description = "Viet Tien Men T-Shirt"
            });
        }
    }
}
