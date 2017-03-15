using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class ResumeExperience
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Company { get; set; }

        public string Location { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool CurrentlyWorking { get; set; }

        public string Description { get; set; }

        [Required]
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
