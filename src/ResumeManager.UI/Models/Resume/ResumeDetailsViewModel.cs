using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResumeManager.DataAccess.Models;

namespace ResumeManager.UI.Models.Resume
{
    public class ResumeDetailsViewModel
    {
        public int ResumeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public List<ResumeLanguage> Languages { get; set; }
        public string Skills { get; set; }
        public string Summary { get; set; }
        public string Jobs { get; set; }
        public string Qualifications { get; set; }
        public string Courses { get; set; }
        public string Interests { get; set; }
        public string References { get; set; }
    }
}
