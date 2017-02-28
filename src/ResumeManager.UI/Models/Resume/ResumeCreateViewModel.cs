using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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

        public List<ResumeLanguage> Languages { get; set; }

        public string Skills { get; set; }
        public string Summary { get; set; }
        public string Jobs { get; set; }
        public string Qualifications { get; set; }
        public string Courses { get; set; }
        public string Interests { get; set; }
        public string References { get; set; }
    }
}
