using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeManager.UI.Models.DraftExperience
{
    public class UpdateExperienceViewModel
    {
        public int DraftExperienceId { get; set; }        

        [Required]
        public string Title { get; set; }

        [Required]
        public string Company { get; set; }

        public string Location { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public bool CurrentlyWorking { get; set; }

        public string Description { get; set; }
    }
}