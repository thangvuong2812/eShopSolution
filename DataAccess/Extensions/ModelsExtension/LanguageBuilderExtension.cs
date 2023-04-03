using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Extensions.ModelsExtension { 
    public class LanguageBuilderExtension : IModelBuilderExtensions
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().HasData(
           new Language() { Id = "vi", Name = "Tiếng Việt", IsDefault = true },
           new Language() { Id = "en", Name = "English", IsDefault = false });
        }
    }
}
