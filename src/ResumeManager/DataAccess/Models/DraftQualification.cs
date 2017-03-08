using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class DraftQualification
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string InstitutionName { get; set; }
        [Required]
        public DateTime DateAquired { get; set; }
        public string OtherInformation { get; set; }
        public ResumeDraft ResumeDraft { get; set; }
        [Required]
        public int ResumeDraftId { get; set; }
    }
}
