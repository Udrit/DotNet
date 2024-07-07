using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;
using UdritDhakal.Models.Entity;

namespace UdritDhakal.Infrastructure.Entity_Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.CourseName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.CreditHours)
                .IsRequired();

            builder.Property(p => p.FullMarks)
                .IsRequired();

            builder.Property(p => p.CreatedDate)
             .IsRequired()
             .HasDefaultValueSql("GETDATE()")
             .HasColumnType("DATETIME");

            builder.Property(p => p.ModifiedDate)
               .IsRequired()
               .HasDefaultValueSql("GETDATE()")
               .HasColumnType("DATETIME");


        }
    }
}
