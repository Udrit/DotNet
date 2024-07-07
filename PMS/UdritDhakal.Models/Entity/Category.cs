using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdritDhakal.Models.Entity
{
    public class Category : BaseEntity
    {
        [Required]
        public string categoryName { get; set; }
        [Required]
        public string categoryDescription { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public string? CreatedBy { get; set; }
        [Required]
        public bool IsAvaliable { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
