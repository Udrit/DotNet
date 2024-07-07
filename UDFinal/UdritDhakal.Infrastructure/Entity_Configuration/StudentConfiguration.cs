using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdritDhakal.Infrastructure.Entity_Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Batch)
                .IsRequired();

            builder.Property(p => p.SectionId)
                .IsRequired(false);

            builder.Property(p => p.Semester)
                .IsRequired(false);

            builder.Property(p => p.Batch)
               .HasColumnType("DATETIME")
               .IsRequired(false);

            builder.Property(p => p.CreatedDate)
               .IsRequired()
               .HasDefaultValueSql("GETDATE()")
               .HasColumnType("DATETIME");

            builder.Property(p => p.ModifiedDate)
               .IsRequired()
               .HasDefaultValueSql("GETDATE()")
               .HasColumnType("DATETIME");


            builder.Property(p => p.studenturl)
               .IsRequired(false);
            builder.Property(p => p.transcriptPhotoUrl)
              .IsRequired(false);
            builder.Property(p => p.citizenshipPhotoUrl)
              .IsRequired(false);

            builder.HasOne(e => e.Degree)
                 .WithMany(e => e.Students)
                 .HasForeignKey(e => e.DegreeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
