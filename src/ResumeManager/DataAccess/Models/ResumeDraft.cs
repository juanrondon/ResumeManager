using System;
using System.Collections.Generic;

namespace ResumeManager.DataAccess.Models
{
    public class ResumeDraft
    {
        public int Id { get; set; }

        public byte[] Photo { get; set; }

        public string PhotoFileType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Address { get; set; }

        public string GitHub { get; set; }

        public string LinkedIn { get; set; }

        public List<ResumeDraftLanguage> ResumeDraftLanguages { get; set; }

        public List<ResumeDraftSkill> ResumeDraftSkills { get; set; }

        public string PersonalSkills { get; set; }

        public List<DraftJob> DraftJobs { get; set; }

        public List<DraftQualification> DraftQualifications { get; set; }

        public List<DraftCourse> DraftCourses { get; set; }

        public string Interests { get; set; }

        public string References { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public Resume Resume { get; set; }

        public int? ResumeId { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateCompleted { get; set; }

        public string Status { get; set; }

        public ResumeDraft()
        {
            ResumeDraftLanguages = new List<ResumeDraftLanguage>();
            ResumeDraftSkills = new List<ResumeDraftSkill>();
            DraftCourses = new List<DraftCourse>();
            DraftJobs = new List<DraftJob>();
            DraftQualifications = new List<DraftQualification>();
        }
    }
}
