using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ResumeManager.DataAccess.Models;

namespace ResumeManager.DataAccess.Models
{
    public class ResumeDraftInterest
    {
        public int Id { get; set; }
        [Required]
        public int ResumeDraftId { get; set; }
        public ResumeDraft ResumeDraft { get; set; }
        [Required]
        public string InterestName { get; set; }
    }
}
