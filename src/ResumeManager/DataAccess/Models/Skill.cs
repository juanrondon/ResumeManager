using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class Skill
    {
        public int SkillId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
