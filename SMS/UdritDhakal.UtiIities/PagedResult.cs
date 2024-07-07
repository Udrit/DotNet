using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdritDhakal.UtiIities
{
    public class PagedResult<T> where T : class
    {
        public PagedResult() 
        {

        }

        public List<T> Date { get; set; }
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
