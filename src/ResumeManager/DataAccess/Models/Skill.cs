using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class Skill
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
