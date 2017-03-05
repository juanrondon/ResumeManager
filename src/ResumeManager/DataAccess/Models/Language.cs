using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class Language
    {
        public int LanguageId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
