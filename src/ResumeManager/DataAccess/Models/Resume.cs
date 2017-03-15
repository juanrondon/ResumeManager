using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class Resume
    {
        public int Id { get; set; }

        public byte[] Photo { get; set; }

        public string PhotoFileType { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Address { get; set; }

        public string GitHub { get; set; }

        public string LinkedIn { get; set; }

        public List<ResumeLanguage> ResumeLanguages { get; set; }

        public List<ResumeSkill> ResumeSkills { get; set; }

        [Required]
        public string PersonalSkills { get; set; }

        public List<ResumeExperience> ResumeExperiences { get; set; }

        public List<ResumeEducation> ResumeEducations { get; set; }

        public List<ResumeInterest> ResumeInterests { get; set; }

        [Required]
        public string References { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        public List<ResumeDraft> ResumeDrafts { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public Resume()
        {
            ResumeDrafts = new List<ResumeDraft>();
            ResumeLanguages = new List<ResumeLanguage>();
            ResumeSkills = new List<ResumeSkill>();
            ResumeInterests = new List<ResumeInterest>();
            ResumeExperiences = new List<ResumeExperience>();
            ResumeEducations = new List<ResumeEducation>();
        }
    }
}
