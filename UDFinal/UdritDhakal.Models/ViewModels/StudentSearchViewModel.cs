using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdritDhakal.Models.ViewModels
{
    public class StudentSearchViewModel
    {
        public string Query { get; set; }
        public DateTime? Batch { get; set; }
        public int Semester { get; set; }
        public int SectionId { get; set; }
        public int? DegreeId { get; set; }
    }
}
