using System.ComponentModel.DataAnnotations;
using ResumeManager.UI.Models.Validators;

namespace ResumeManager.UI.Models.DraftEducation
{
    public class AddEducationViewModel
    {
        [Required]
        public int ResumeDraftId { get; set; }

        [Required]
        public string School { get; set; }

        [Required]
        public string Degree { get; set; }

        [Display(Name = "Field Of Study")]
        public string FieldOfStudy { get; set; }

        public string Grade { get; set; }

        [Display(Name = "From Year")]
        public int? FromYear { get; set; }

        [Display(Name = "To Year (or expected)")]
        [StartEndYearValidator]
        public int? ToYear { get; set; }

        public string Description { get; set; }
    }
}