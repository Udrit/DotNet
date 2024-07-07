using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdritDhakal.Infrastructure.Entity_Configuration
{
    public class DegreeConfiguration : IEntityTypeConfiguration<Degree>
    {
        public void Configure(EntityTypeBuilder<Degree> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.DegreeName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.No_Of_Years)
                .IsRequired();

            builder.Property(p => p.No_Of_Semesters)
                .IsRequired();


            builder.HasMany(e => e.Students)
                .WithOne(e => e.Degree)
                .HasForeignKey(e => e.DegreeId);

            builder.HasOne(e => e.Discipline)
                 .WithMany(e => e.Degrees)
                 .HasForeignKey(e => e.DisciplineId);


            builder.Property(p => p.StartDate)
             .HasColumnType("DATE");

            builder.Property(p => p.CreatedDate)
             .IsRequired()
             .HasDefaultValueSql("GETDATE()")
             .HasColumnType("DATETIME");

            builder.Property(p => p.ModifiedDate)
               .IsRequired()
               .HasDefaultValueSql("GETDATE()")
               .HasColumnType("DATETIME");

            builder.HasMany(e => e.Courses)
                .WithMany(e => e.Degrees)
                .UsingEntity<DegreeCourse>(
                l => l.HasOne(e => e.Course).WithMany(e => e.DegreeCourses).HasForeignKey(e => e.CourseId).OnDelete(DeleteBehavior.Cascade),
                r => r.HasOne(e => e.Degree).WithMany(e => e.DegreeCourses).HasForeignKey(e => e.DegreeId).OnDelete(DeleteBehavior.Cascade)
                );




        }
    }
}
