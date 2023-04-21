using DataAccess.Extensions.ModelsExtension;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Extensions
{
    public static class ModelsBuilderRepository
    {
        //Single principle
        public static void Seed(this ModelBuilder modelBuilder)
        {
            new AppConfigBuilderExtension().Seed(modelBuilder);
            new CategoryBuilderExtension().Seed(modelBuilder);

            new CategoryTranslationBuilderExtension().Seed(modelBuilder);

            new LanguageBuilderExtension().Seed(modelBuilder);

            new ProductBuilderExtension().Seed(modelBuilder);

            new ProductInCategoryBuilderExtension().Seed(modelBuilder);

            new RoleBuilderExtension().Seed(modelBuilder);
            new SlideBuilderExtension().Seed(modelBuilder);
            new UserBuilderExtension().Seed(modelBuilder);
            new UserRoleBuilderExtension().Seed(modelBuilder);

            new ProductTranslationBuilderExtension().Seed(modelBuilder);
        }
    }
}
