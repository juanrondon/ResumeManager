using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class ResumeDraftSkill
    {
        public int Id { get; set; }
        [Required]
        public int ResumeDraftId { get; set; }
        public ResumeDraft ResumeDraft { get; set; }
        [Required]
        public string SkillName { get; set; }
    }
}
