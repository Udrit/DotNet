using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdritDhakal.Infrastructure.Entity_Configuration
{
    public class DegreeCourseConfiguration : IEntityTypeConfiguration<DegreeCourse>
    {
        public void Configure(EntityTypeBuilder<DegreeCourse> builder)
        {
            builder.HasAlternateKey(p => new { p.DegreeId, p.CourseId });

        }
    }
}
