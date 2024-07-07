using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdritDhakal.Models.ViewModels
{
    public class DegreeSearchViewModel
    {
        public string Query { get; set; }
        public int CourseId { get; set; }
        public string Level { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
