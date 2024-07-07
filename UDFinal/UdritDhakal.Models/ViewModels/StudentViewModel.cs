using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdritDhakal.Models.Entity;

namespace UdritDhakal.Models.ViewModels
{
    public class StudentViewModel
    {
        public StudentSearchViewModel studentSearchViewModel { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}
