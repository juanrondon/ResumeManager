using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResumeManager.DataAccess.Models;

namespace ResumeManager.UI.Models.Resume
{
    public class ResumeCreateViewModel
    {
        public int ResumeId { get; set; }
        [Required(ErrorMessage = "First Name field is required")]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        public string Photo { get; set; }
        

        [Required(ErrorMessage = "Last Name field is required")]
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email field is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile field is required")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Address field is required")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Languages")]
        public List<int> LanguageListIds { get; set; }

        public MultiSelectList LanguageList { get; set; }
        public string GitHub { get; set; }

        public string LinkedIn { get; set; }
        [Required]
        [Display(Name = "Skill")]
        public string CoreSkills { get; set; }             

        //[Required(ErrorMessage = "Summary field is required")]
        //public string Summary { get; set; }
        //[Required]
        //public string Jobs { get; set; }
        //public string Qualifications { get; set; }
        //public string Courses { get; set; }
        //[Required]
        //public string Interests { get; set; }
        //public string References { get; set; }
    }
}
