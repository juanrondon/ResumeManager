using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class Resume
    {
        public int ResumeId { get; set; }
        //contact details
        public byte[] Photo { get; set; }
        public string PhotoFileType { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Address { get; set; }
        public string GitHub { get; set; }
        public string LinkedIn { get; set; }

        public List<ResumeLanguage> ResumeLanguages { get; set; }

        //Skills
        public List<Skill> CoreSkills { get; set; }

        public string Summary { get; set; }

        //Experience
        public List<Job> Jobs { get; set; }

        //Qualifications
        public List<Qualification> Qualifications { get; set; }

        //Education
        public List<Course> Courses { get; set; }

        //Other
        public string Interests { get; set; }

        public string References { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
