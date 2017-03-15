using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ResumeManager.DataAccess.Models;

namespace ResumeManager.DataAccess.Models
{
    public class ResumeInterest
    {
        public int Id { get; set; }
        [Required]
        public int ResumeId { get; set; }
        public ResumeDraft Resume { get; set; }
        [Required]
        public string InterestName { get; set; }
    }
}
