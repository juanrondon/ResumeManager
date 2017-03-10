using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeManager.UI.Models.DraftQualification
{
    public class AddQualificationViewModel
    {
        [Required]
        public int ResumeDraftId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Institution { get; set; }
        [Required]
        [Display(Name ="Date Aquired")]
        public DateTime DateAquired { get; set; }
        [Display(Name = "Other Information")]
        public string OtherInfo { get; set; }

    }
}