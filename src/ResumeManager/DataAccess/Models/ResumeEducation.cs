using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class ResumeEducation
    {
        public int Id { get; set; }
        [Required]
        public string School { get; set; }
        [Required]
        public string Degree { get; set; }

        public string FieldOfStudy { get; set; }

        public string Grade { get; set; }
        public int? FromYear { get; set; }
        public int? ToYear { get; set; }

        public string Description { get; set; }

        public Resume Resume { get; set; }
        [Required]
        public int ResumeId { get; set; }
    }
}
