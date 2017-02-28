using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public string InstitutionName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OtherInformation { get; set; }
    }
}
