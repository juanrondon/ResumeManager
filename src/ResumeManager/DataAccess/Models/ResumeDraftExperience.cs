using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class ResumeDraftExperience
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CurrentlyWorking { get; set; }
        public string Description { get; set; }

        [Required]
        public int ResumeDraftId { get; set; }
        public ResumeDraft ResumeDraft { get; set; }
    }
}
