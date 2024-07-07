using UdritDhakal.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdritDhakal.Models.ViewModels
{
    public class ProductViewModel
    {
        public SearchVIewModel searchViewModel { get; set; }
        public IEnumerable<Product> products { get; set; }
    }
}
