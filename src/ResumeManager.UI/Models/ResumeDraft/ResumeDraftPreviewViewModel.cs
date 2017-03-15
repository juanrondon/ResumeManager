using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResumeManager.UI.Models.DraftEducation;

namespace ResumeManager.UI.Models.ResumeDraft
{
    public class ResumeDraftPreviewViewModel
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Address { get; set; }

        public string GitHub { get; set; }

        public string LinkedIn { get; set; }

        public string PersonalSkills { get; set; }

        public  List<string> Languages { get; set; }

        public List<string> Skills { get; set; }

        public List<PreviewEducationViewModel> DraftEducations { get; set; }
    }
}
