using System.ComponentModel.DataAnnotations;
using ResumeManager.UI.Models.Validators;

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
        public int? StartYear { get; set; }

        [Display(Name = "End Date")]        
        public int? EndYear { get; set; }

        public bool? CurrentlyWorking { get; set; }

        public string Description { get; set; }
    }
}