using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdritDhakal.Models.Entity;

namespace UdritDhakal.Models.ViewModels
{
    public class DegreeViewModel
    {
        public DegreeSearchViewModel degreeSearchViewModel { get; set; }
        public IEnumerable<Degree> Degree { get; set; }
    }
}
