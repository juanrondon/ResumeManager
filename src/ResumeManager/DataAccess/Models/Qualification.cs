using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class Qualification
    {
        public int QualificationId { get; set; }
        [Required]
        public string QualificationName { get; set; }
        [Required]
        public string QualificationType { get; set; }
        [Required]
        public string InstitutionName { get; set; }
        [Required]
        public DateTime DateAquired { get; set; }
        public string OtherInformation { get; set; }
    }
}
