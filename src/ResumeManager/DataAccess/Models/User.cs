using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
