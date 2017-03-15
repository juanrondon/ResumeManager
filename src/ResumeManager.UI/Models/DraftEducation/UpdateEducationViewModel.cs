using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResumeManager.UI.Models.Validators;

namespace ResumeManager.UI.Models.DraftEducation
{
    public class UpdateEducationViewModel
    {
        public int DraftEducationId { get; set; }

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
        [StartEndYearValidatorModal]
        public int? ToYear { get; set; }

        public string Description { get; set; }
    }
}