using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdritDhakal.Infrastructure;

namespace UdritDhakal.Models.Entity
{
    public class Course : BaseEntity
    {
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public int CreditHours { get; set; }
        public int FullMarks { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public virtual IEnumerable<Degree> Degrees { get; set; }
        public virtual IEnumerable<DegreeCourse> DegreeCourses { get; set; }

    }
}
