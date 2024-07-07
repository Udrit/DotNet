using UdritDhakal.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdritDhakal.Infrastructure.Entity_Configuration
{
    public class ProductConfiguration:IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(true);

            builder.Property(e => e.ProductDescription)
                .HasMaxLength(300)
                .IsUnicode(true);

            builder.Property(e => e.Rate);
            builder.Property(e => e.quantity);
            

            builder.Property(e => e.batchNo)
                .HasMaxLength(50)
                .IsUnicode(true);

            builder.Property(e => e.productionDate);
       //    .HasColumnType("GetDate()");

            builder.Property(e => e.ExpiryDate);
          //    .HasColumnType("datetime");

            builder.Property(e => e.ImageUrl)
                .HasMaxLength(300)
                .IsUnicode(true);
            builder.Property(e => e.IsAvailable);

            builder.HasOne(e => e.Category)
                 .WithMany(s => s.Products)
                 .HasForeignKey(e => e.CategoryId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
