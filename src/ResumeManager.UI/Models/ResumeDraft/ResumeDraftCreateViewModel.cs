using Microsoft.AspNetCore.Mvc.Rendering;
using ResumeManager.UI.Models.DraftEducation;
using ResumeManager.UI.Models.DraftExperience;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResumeManager.UI.Models.ResumeDraft
{
    public class ResumeDraftCreateViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        public string Address { get; set; }

        [Display(Name = "Languages")]
        public List<int> LanguageListIds { get; set; }

        public MultiSelectList LanguageList { get; set; }

        public EducationViewModels EducationViewModels { get; set; }

        public ExperienceViewModels ExperienceViewModels { get; set; }

        public string GitHub { get; set; }

        public string LinkedIn { get; set; }

        public string PersonalSkills { get; set; }

        public string References { get; set; }

        public string ProfilePhotoBase64 { get; set; }

        [Required]
        public string Skill { get; set; }

        [Required]
        public string Interest { get; set; }

        public ResumeDraftCreateViewModel()
        {
            EducationViewModels = new EducationViewModels();
            ExperienceViewModels = new ExperienceViewModels();
        }
    }
}