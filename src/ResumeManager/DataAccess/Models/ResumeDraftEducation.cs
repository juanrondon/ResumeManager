using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class ResumeDraftEducation
    {
        public int Id { get; set; }
        public string School { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public string Grade { get; set; }
        public int? FromYear { get; set; }
        public int? ToYear { get; set; }
        public string Description { get; set; }

        public ResumeDraft ResumeDraft { get; set; }
        [Required]
        public int ResumeDraftId { get; set; }
    }
}
