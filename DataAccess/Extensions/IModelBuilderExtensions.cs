using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Extensions
{
    public interface IModelBuilderExtensions
    {
        void Seed(ModelBuilder modelBuilder);
    }
}
