using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class ResumeDraftLanguage
    {
        public int Id { get; set; }
        [Required]
        public int ResumeDraftId { get; set; }
        public ResumeDraft ResumeDraft { get; set; }
        [Required]
        public string LanguageName { get; set; }
    }
}
