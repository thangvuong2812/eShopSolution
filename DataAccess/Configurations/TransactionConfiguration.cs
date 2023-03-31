using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions").HasKey(o => o.Id);
            builder.HasIndex(o => o.Id);
            builder.Property(o => o.Id).UseIdentityColumn();

            builder.HasOne(o => o.User).WithMany(o => o.Transactions).HasForeignKey(o => o.UserId);
        }
    }
}
