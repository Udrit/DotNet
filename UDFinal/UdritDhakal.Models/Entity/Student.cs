using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdritDhakal.Infrastructure;

namespace UdritDhakal.Models.Entity
{
    public class Student : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Batch { get; set; }
        public int? Semester { get; set; }
        public int? SectionId { get; set; }
        public bool IsEnrolled { get; set; }
        public string? studenturl { get; set; }
        public string? transcriptPhotoUrl { get; set; }
        public string? citizenshipPhotoUrl { get; set; }
        [NotMapped]
        public IFormFile? studentPhoto { get; set; }
        [NotMapped]
        public IFormFile? transcriptPhoto { get; set; }
        [NotMapped]
        public IFormFile? citizenshipPhoto { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public int DegreeId { get; set; }
        public virtual Degree Degree { get; set; }
    }
}
