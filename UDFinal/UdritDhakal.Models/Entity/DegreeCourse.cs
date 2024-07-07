using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdritDhakal.Infrastructure;

namespace UdritDhakal.Models.Entity
{
    public class DegreeCourse : BaseEntity
    {
        public int DegreeId { get; set; }
        public int CourseId { get; set; }
        public int Semester { get; set; }
        public virtual Degree Degree { get; set; }
        public virtual Course Course { get; set; }
    }
}

