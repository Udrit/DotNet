using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdritDhakal.Models.Entity;

namespace UdritDhakal.Models.ViewModels
{
    public class CourseSearchViewModel
    {
        public int Semester { get; set; }
        public int DegreeId { get; set; }
        public int CourseId { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}