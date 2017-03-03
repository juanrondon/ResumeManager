using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class ResumeLanguage
    {
        [Required]
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
        [Required]
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
