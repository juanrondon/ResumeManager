using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }

        public List<Resume> Resumes { get; set; }

        public List<ResumeDraft> ResumeDrafts { get; set; }
    }
}
