using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ResumeManager.DataAccess.Models
{
    public class FieldOfStudy
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
