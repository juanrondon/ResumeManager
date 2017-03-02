using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ResumeManager.UI.Models.Resume
{
    public class ResumeCreateContactDetailsViewModel
    {
        [Required(ErrorMessage = "First Name field is required")]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

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
    }
}
