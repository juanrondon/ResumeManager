using System.ComponentModel.DataAnnotations;
using ResumeManager.UI.Models.Validators;
using System;

namespace ResumeManager.UI.Models.DraftExperience
{
    public class AddExperienceViewModel
    {
        [Required]
        public int ResumeDraftId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Company { get; set; }

        public string Location { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name ="Still Working Here")]
        public bool CurrentlyWorking { get; set; }

        public string Description { get; set; }
    }
}